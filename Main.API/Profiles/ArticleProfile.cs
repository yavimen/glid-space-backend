using AutoMapper;
using Main.API.DtoModels;
using Main.API.Persistance;

namespace Main.API.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ReverseMap();
            CreateMap<ArticleForCreationDto, Article>();
        }
    }
}