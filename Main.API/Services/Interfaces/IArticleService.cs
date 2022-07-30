using Main.API.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace Main.API.Services.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetAllArticles();

        Task<ArticleDto> GetArticleById(int id);

        Task<ArticleDto> AddArticle(ArticleForCreationDto article);

        Task DeleteArticleById(int id);

        Task UpgrateArticle(int id, ArticleDto article);
    }
}
