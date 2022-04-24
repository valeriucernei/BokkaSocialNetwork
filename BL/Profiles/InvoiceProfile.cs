using AutoMapper;
using Common.Dtos.Invoice;
using Domain.Models;

namespace BL.Profiles;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<InvoiceForUpdateDto, Invoice>();
    }
}