using AutoMapper;
using OnlineCourseSystem.Entities;
using OnlineCourseSystem.Utility;
using OnlineCourseSystem.ViewModels.Service;
namespace OnlineCourseSystem;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            #region Service
            config.CreateMap<Service, ServiceViewModel>()
                .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnServiceImagePath(src.Image)));
            config.CreateMap<Service, AddEditServiceViewModel>().ReverseMap();
            #endregion
        });
        return mappingConfig;
    }
}
