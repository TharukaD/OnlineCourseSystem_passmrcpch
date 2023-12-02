using AutoMapper;
using OnlineCourseSystem.Entities;
using OnlineCourseSystem.Utility;
using OnlineCourseSystem.ViewModels.Article;
using OnlineCourseSystem.ViewModels.ArticleCategory;
using OnlineCourseSystem.ViewModels.CounterRecord;
using OnlineCourseSystem.ViewModels.HomePageBanner;
using OnlineCourseSystem.ViewModels.Service;
using OnlineCourseSystem.ViewModels.Tag;
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

            #region HomePageBanner
            config.CreateMap<HomePageBanner, HomePageBannerViewModel>()
                .ForMember(r => r.SmallImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnHomePageBannerSmallImagePath(src.SmallImage)))
                .ForMember(r => r.LargeImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnHomePageBannerLargeImagePath(src.LargeImage)));
            config.CreateMap<HomePageBanner, AddEditHomePageBannerViewModel>().ReverseMap();
            #endregion

            #region CounterRecord
            config.CreateMap<CounterRecord, CounterRecordViewModel>()
                .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnCounterRecordImagePath(src.Image)));
            config.CreateMap<CounterRecord, AddEditCounterRecordViewModel>().ReverseMap();
            #endregion

            #region Tag
            config.CreateMap<Tag, TagViewModel>();
            config.CreateMap<Tag, AddEditTagViewModel>().ReverseMap();
            #endregion

            #region Article Category
            config.CreateMap<ArticleCategory, ArticleCategoryViewModel>();
            config.CreateMap<ArticleCategory, AddEditArticleCategoryViewModel>().ReverseMap();
            #endregion

            #region Article
            config.CreateMap<Article, ArticleViewModel>()
                .ForMember(r => r.PublishedDateString, opt => opt.MapFrom(src => HelperMethods.ToDateString(src.PublishedDate)))
                .ForMember(r => r.ArticleCategoryName, opt => opt.MapFrom(src => src.ArticleCategory.Name))
                .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnArticleImagePath(src.Image)))
                .ForMember(r => r.Tags, opt => opt.MapFrom(src => src.ArticleTags.Select(r => new TagViewModel { Id = r.Id, Name = r.Tag.Name })));
            config.CreateMap<Article, AddEditArticleViewModel>().ReverseMap();
            #endregion

        });
        return mappingConfig;
    }
}
