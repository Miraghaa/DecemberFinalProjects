using AutoMapper;
using December.Business.ViewModels.ContactVMs;
using December.Core.Entities;

namespace December.Business.Mappers;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactCreateVM>().ReverseMap();
    }
}