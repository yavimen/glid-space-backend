using AutoMapper;
using Main.API.DtoModels;
using Main.API.Persistance;
using Main.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.API.Services
{
    public class ArticleService : IArticleService
    {
        private readonly MainDbContext _dbContext;
        private readonly IMapper _mapper;

        public ArticleService(MainDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleDto>> GetAllArticles()
        {
            var articles = await _dbContext.Articles.ToListAsync();

            var articleDtos = _mapper.Map<IEnumerable<ArticleDto>>(articles);

            return articleDtos;
        }

        public async Task<ArticleDto> GetArticleById(int ArticleId)
        {
            var article = await _dbContext.Articles.FirstOrDefaultAsync(artc => artc.Id == ArticleId);

            var articleDto = _mapper.Map<ArticleDto>(article);

            return articleDto;
        }

        public async Task<ArticleDto> AddArticle(ArticleForCreationDto article)
        {
            if (!article.IsFilledFields())
                return null; 

            var articleEntity = _mapper.Map<Article>(article);

            await _dbContext.Articles.AddAsync(articleEntity);
            await _dbContext.SaveChangesAsync();

            var createdArticle = _mapper.Map<ArticleDto>(articleEntity);

            return createdArticle;
        }

        public async Task DeleteArticleById(int id)
        {
            var requestedArticle = await _dbContext.Articles.FirstOrDefaultAsync(arcl => arcl.Id == id);

            if (requestedArticle == null)
                throw new Exception("Article not found.");

            _dbContext.Articles.Remove(requestedArticle);
            await _dbContext.SaveChangesAsync();

            return;
        }

        public async Task UpgrateArticle(int id, ArticleDto article)
        {
            var requestedArticle = await _dbContext.Articles.FirstOrDefaultAsync(arcl => arcl.Id == id);

            if (requestedArticle == null)
                throw new Exception("Article not found.");

            if (article.Content == String.Empty)
                throw new Exception("Content is empty.");

            if (article.Title == String.Empty)
                throw new Exception("Title is empty.");

            if (article.PublicationDate == new DateTime())
                throw new Exception("Publication date is empty.");

            requestedArticle.Title = article.Title;
            requestedArticle.Content = article.Content;
            requestedArticle.PublicationDate = article.PublicationDate;

            await _dbContext.SaveChangesAsync();

            return;
        }
    }
}
