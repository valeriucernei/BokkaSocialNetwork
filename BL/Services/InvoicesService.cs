using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Invoice;
using Common.Dtos.Subscription;
using Common.Enums;
using Common.Exceptions;
using Common.Models;
using DataAccess.Interfaces;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Invoice = Domain.Models.Invoice;
using Subscription = Stripe.Subscription;

namespace BL.Services;

public class InvoicesService : IInvoicesService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;
    private readonly UserManager<User> _userManager;
    private readonly ISubscriptionsService _subscriptionsService;
    private readonly IInvoicesRepository _invoicesRepository;
    private readonly StripeSettings _stripeSettings;
    
    public InvoicesService(
        IRepository repository, 
        IMapper mapper, 
        IUsersService usersService,
        UserManager<User> userManager,
        ISubscriptionsService subscriptionsService,
        IInvoicesRepository invoicesRepository,
        IOptions<StripeSettings> stripeSettings)
    {
        _repository = repository;
        _mapper = mapper;
        _usersService = usersService;
        _userManager = userManager;
        _subscriptionsService = subscriptionsService;
        _invoicesRepository = invoicesRepository;
        _stripeSettings = stripeSettings.Value;
    }

    public async Task<InvoiceDto?> GetInvoiceById(Guid id)
    {
        var invoice = await _repository.GetById<Invoice>(id);
    
        if (invoice is null)
            throw new NotFoundException("There is no invoice with such Id.");
        
        var result = _mapper.Map<InvoiceDto>(invoice);
    
        return result;
    }
    
    public async Task<List<InvoiceDto>> GetPersonalInvoices(ClaimsPrincipal userClaims)
    {
        var user = await _usersService.GetUserByClaims(userClaims);
    
        var invoices = await _invoicesRepository.GetInvoicesByUserId(user.Id);
        
        var result = _mapper.Map<List<InvoiceDto>>(invoices);
    
        return result;
    }

    public async Task<CreateCheckoutSessionResponseDto> CreateCheckoutSession(CreateCheckoutSessionRequestDto req)
    {
        var session = await CreateSession(req);
        var result = new CreateCheckoutSessionResponseDto
        {
            SessionId = session.Id,
            PublicKey = _stripeSettings.PublicKey,
        };

        return result;
    }

    public async Task StripeWebhook(Event stripeEvent)
    {
        switch (stripeEvent.Type)
        {
            case Events.CustomerCreated:
                await HandleCustomerCreated(stripeEvent);
                break;

            case Events.InvoiceCreated:
                await HandleInvoiceCreated(stripeEvent);
                break;

            case Events.CustomerSubscriptionCreated:
                await HandleCustomerSubscriptionCreated(stripeEvent);
                break;
            
            case Events.InvoicePaid:
                await HandleInvoicePaid(stripeEvent);
                break;
            
            case Events.InvoicePaymentFailed:
                await HandleInvoicePaymentFailed(stripeEvent);
                break;
        }
    }

    private async Task HandleCustomerCreated(Event stripeEvent)
    {
        var customer = stripeEvent.Data.Object as Customer;
        var user = await _userManager.FindByEmailAsync(customer!.Email);

        user.StripeCustomerId = customer.Id;
        await _repository.SaveChangesAsync();
    }

    private async Task HandleInvoiceCreated(Event stripeEvent)
    {
        var stripeInvoice = stripeEvent.Data.Object as Stripe.Invoice;

        var user = await _userManager.FindByEmailAsync(stripeInvoice!.CustomerEmail);
                
        var invoice = new Invoice()
        {
            Amount = (int)stripeInvoice.Total,
            User = user
        };
                
        await _repository.Add(invoice);
        await _repository.SaveChangesAsync();
    }

    private async Task HandleCustomerSubscriptionCreated(Event stripeEvent)
    {
        var service = new CustomerService();
        var subscription = stripeEvent.Data.Object as Subscription;
        var customer = await service.GetAsync(subscription!.CustomerId);

        var subscriptionCreateDto = new SubscriptionCreateDto
        {
            Email = customer.Email,
            StartDateTime = subscription.CurrentPeriodStart,
            EndDateTime = subscription.CurrentPeriodEnd
        };

        await _subscriptionsService.CreateSubscription(subscriptionCreateDto);
    }

    private async Task HandleInvoicePaid(Event stripeEvent)
    {
        var stripeInvoice = stripeEvent.Data.Object as Stripe.Invoice;
        var invoice = await _invoicesRepository.GetInvoiceByStripeInvoiceId(stripeInvoice!.Id);

        invoice!.Status = InvoiceStatus.Success;
        await _repository.SaveChangesAsync();
    }

    private async Task HandleInvoicePaymentFailed(Event stripeEvent)
    {
        var stripeInvoice = stripeEvent.Data.Object as Stripe.Invoice;
        var invoice = await _invoicesRepository.GetInvoiceByStripeInvoiceId(stripeInvoice!.Id);

        invoice!.Status = InvoiceStatus.Fail;
        await _repository.SaveChangesAsync();
    }

    private async Task<Session> CreateSession(CreateCheckoutSessionRequestDto req)
    {
        var user = await _userManager.FindByIdAsync(req.ClientReferenceId);
        
        var options = new SessionCreateOptions
        {
            SuccessUrl = req.SuccessUrl,
            CancelUrl = req.FailureUrl,
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
            Mode = "subscription",
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = req.PriceId,
                    Quantity = 1,
                },
            },
            ClientReferenceId = req.ClientReferenceId,
            CustomerEmail = user.StripeCustomerId is null ? req.CustomerEmail : null,
            Customer = user.StripeCustomerId
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session;
    }
}