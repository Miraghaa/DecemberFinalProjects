using AutoMapper;
using December.Business.ViewModels.AccountVMs;
using December.Business.ViewModels.ContactVMs;
using December.Core.Entities;

namespace December.Business.Mappers;

public class AdressProfile : Profile
{
    public AdressProfile()
    {
        CreateMap<Adress, AdressVM>().ReverseMap();
        CreateMap<AdressVM,Adress>().ReverseMap();
    }
}
