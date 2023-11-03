using AutoMapper;
using December.Business.ViewModels.AreasViewModels.SliderVMs;
using December.Core.Entities.Areas;

namespace December.Business.Mappers;

public class SliderProfile:Profile
{
    public SliderProfile()
    {
        CreateMap<Slider,SliderCreateVM>().ReverseMap();
        CreateMap<Slider,SliderUpdateVM>().ReverseMap();
    }
}
