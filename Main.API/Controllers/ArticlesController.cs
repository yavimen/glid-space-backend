using AutoMapper;
using Main.API.DtoModels;
using Main.API.Persistance;
using Main.API.Services;
using Main.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.API.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticles();

            if (articles.ToList().Count == 0)
                return Ok("List of articles is empty");

            return Ok(articles);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleById(id);

            if (article == null)
                return NotFound("Article with this id does not exist");

            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewArticle([FromBody] ArticleForCreationDto article)
        {
            var newArticle = await _articleService.AddArticle(article);

            if (newArticle == null)
                return BadRequest("Not all fields are filled");

            return Ok(newArticle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleById(int id) 
        {
            try
            {
                await _articleService.DeleteArticleById(id);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok("Article deleted");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpgrateArticle(int id, ArticleDto article) 
        {
            try
            {
                await _articleService.UpgrateArticle(id, article);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok("Article updated.");
        }
             
    }
}
