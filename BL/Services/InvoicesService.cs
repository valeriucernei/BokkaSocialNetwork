using System.Security.Claims;
using AutoMapper;
using BL.Interfaces;
using Common.Dtos.Invoice;
using Common.Dtos.Subscription;
using Common.Exceptions;
using Common.Models;
using DataAccess.Interfaces;
using Domain.Models;
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
    private readonly ISubscriptionsService _subscriptionsService;
    private readonly IInvoicesRepository _invoicesRepository;
    private readonly StripeSettings _stripeSettings;
    
    public InvoicesService(
        IRepository repository, 
        IMapper mapper, 
        IUsersService usersService,
        ISubscriptionsService subscriptionsService,
        IInvoicesRepository invoicesRepository,
        IOptions<StripeSettings> stripeSettings)
    {
        _repository = repository;
        _mapper = mapper;
        _usersService = usersService;
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
    
    // public async Task<InvoiceDto> CreateInvoice(InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims)
    // {
    //     var invoice = _mapper.Map<Invoice>(invoiceForUpdateDto);
    //
    //     invoice.User = await _usersService.GetUserByClaims(userClaims);
    //     
    //     await _repository.Add(invoice);
    //     
    //     await _repository.SaveChangesAsync();
    //     
    //     var result = _mapper.Map<InvoiceDto>(invoice);
    //
    //     return result;
    // }
    //
    // public async Task<InvoiceDto> UpdateInvoice(Guid id, InvoiceForUpdateDto invoiceForUpdateDto, ClaimsPrincipal userClaims)
    // {
    //     var invoice = await _repository.GetById<Invoice>(id);
    //
    //     if (invoice is null)
    //         throw new NotFoundException("There is no invoice with such Id.");
    //
    //     var user = await _usersService.GetUserByClaims(userClaims);
    //
    //     if (user.Id != invoice.UserId)
    //         throw new NotAllowedException("You are not allowed to edit this invoice.");
    //
    //     _mapper.Map(invoiceForUpdateDto, invoice);
    //     
    //     await _repository.SaveChangesAsync();
    //     
    //     var result = _mapper.Map<InvoiceDto>(invoice);
    //
    //     return result;
    // }

    public async Task<CreateCheckoutSessionResponseDto> CreateCheckoutSession(CreateCheckoutSessionRequestDto req, ClaimsPrincipal userClaims)
    {
        var session = await CreateSession(req);
        
        if (session is null)
            throw new NotFoundException("There is no plan with such Id.");
        
        var user = await _usersService.GetUserByClaims(userClaims);

        var invoice = new Invoice()
        {
            Amount = req.Price,
            SessionId = session.Id,
            User = user
        };
        
        await _repository.Add(invoice);
        await _repository.SaveChangesAsync();
        
        var result = new CreateCheckoutSessionResponseDto
        {
            SessionId = session.Id,
            PublicKey = _stripeSettings.PublicKey
        };

        return result;
    }

    public async Task<object> StripeWebhook(Event stripeEvent)
    {
        switch (stripeEvent.Type)
        {
            case Events.CustomerSubscriptionCreated:
            {
                var service = new CustomerService();
                var subscription = stripeEvent.Data.Object as Subscription;
                var customer = await service.GetAsync(subscription!.CustomerId);
                

                var subscriptionCreateDto = new SubscriptionCreateDto
                {
                    Email = customer.Email,
                    StartDateTime = subscription!.CurrentPeriodStart,
                    EndDateTime = subscription.CurrentPeriodEnd
                };

                await _subscriptionsService.CreateSubscription(subscriptionCreateDto);
                
                break;
            }
            case Events.CustomerSubscriptionUpdated:
            {
                var session = stripeEvent.Data.Object as Stripe.Subscription;
                Console.WriteLine("*****************Subscription updated*****************");
                // Update Subsription
                //await updateSubscription(session);
                break;
            }
            case Events.CustomerCreated:
            {
                var customer = stripeEvent.Data.Object as Customer;
                Console.WriteLine("*****************Created new customer*****************");
                //Do Stuff
                //await addCustomerIdToUser(customer);
                break;
            }
            case Events.InvoicePaymentFailed:
                Console.WriteLine("*****************Invoice payment failed*****************");
                break;
            default:
                Console.WriteLine("***************** Unhandled event type: {0} *****************", stripeEvent.Type);
                break;
        }

        return stripeEvent;
    }

    private async Task<Session> CreateSession(CreateCheckoutSessionRequestDto req)
    {
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
            CustomerEmail = req.CustomerEmail
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session;
    }


    // private async Task updateSubscription(Subscription subscription)
    // {
    //     var subscriptionFromDb = await _subscriberRepository.GetByIdAsync(subscription.Id);
    //     if (subscriptionFromDb != null)
    //     {
    //         subscriptionFromDb.Status = subscription.Status;
    //         subscriptionFromDb.CurrentPeriodEnd = subscription.CurrentPeriodEnd;
    //         await _subscriberRepository.UpdateAsync(subscriptionFromDb);
    //         Console.WriteLine("Subscription Updated");
    //     }
    //
    // }
    //
    // private async Task addCustomerIdToUser(Customer customer)
    // {
    //     try
    //     {
    //         var userFromDb = await _userManager.FindByEmailAsync(customer.Email);
    //
    //         if (userFromDb != null)
    //         {
    //             userFromDb.CustomerId = customer.Id;
    //             await _userManager.UpdateAsync(userFromDb);
    //             Console.WriteLine("Customer Id added to user ");
    //         }
    //
    //     }
    //     catch (System.Exception ex)
    //     {
    //         Console.WriteLine("Unable to add customer id to user");
    //         Console.WriteLine(ex);
    //     }
    // }
    //
    // private async Task addSubscriptionToDb(Subscription subscription)
    // {
    //     try
    //     {
    //         var subscriber = new Subscriber
    //         {
    //             Id = subscription.Id,
    //             CustomerId = subscription.CustomerId,
    //             Status = "active",
    //             CurrentPeriodEnd = subscription.CurrentPeriodEnd
    //         };
    //         await _subscriberRepository.CreateAsync(subscriber);
    //
    //         //You can send the new subscriber an email welcoming the new subscriber
    //     }
    //     catch (System.Exception ex)
    //     {
    //         Console.WriteLine("Unable to add new subscriber to Database");
    //         Console.WriteLine(ex.Message);
    //     }
    // }
}