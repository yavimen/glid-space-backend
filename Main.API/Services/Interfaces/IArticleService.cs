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

        Task UpdateArticle(int id, ArticleForUpdateDto article);

        Task<bool> IsArticleExist(int id);
    }
}
