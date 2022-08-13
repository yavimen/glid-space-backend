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
            var articles = await _dbContext.Articles
                .OrderBy(a => a.PublicationDate)
                .ToListAsync();

            var articleDtos = _mapper.Map<IEnumerable<ArticleDto>>(articles);

            return articleDtos;
        }

        public async Task<ArticleDto> GetArticleById(int ArticleId)
        {
            var article = await _dbContext.Articles
                .FirstOrDefaultAsync(a => a.Id == ArticleId);

            var articleDto = _mapper.Map<ArticleDto>(article);

            return articleDto;
        }

        public async Task<ArticleDto> AddArticle(ArticleForCreationDto article)
        {
            var articleEntity = _mapper.Map<Article>(article);

            await _dbContext.Articles.AddAsync(articleEntity);

            await _dbContext.SaveChangesAsync();

            var createdArticle = _mapper.Map<ArticleDto>(articleEntity);

            return createdArticle;
        }

        public async Task DeleteArticleById(int id)
        {
            var requestedArticle = await _dbContext.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            _dbContext.Articles.Remove(requestedArticle);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateArticle(int id, ArticleForUpdateDto article)
        {
            var requestedArticle = await _dbContext.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            requestedArticle = _mapper.Map<Article>(article);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsArticleExist(int id) 
        {
            var requestedArticle = await _dbContext.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            return requestedArticle == null ? false : true;
        }
    }
}
