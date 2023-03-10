using FluentValidation;
using Main.API.DtoModels;
using Main.API.Extensions;
using Main.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Main.API.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IValidator<ArticleDto> _articleDtoValidator;
        private readonly IValidator<ArticleForCreationDto> _articleForCreationDtoValidator;
        private readonly IValidator<ArticleForUpdateDto> _articleForUpdateDtoValidator;

        public ArticlesController(IArticleService articleService, IValidator<ArticleDto> articleDtoValidator,
            IValidator<ArticleForCreationDto> articleForCreationDtoValidator,
            IValidator<ArticleForUpdateDto> articleForUpdateValidator)
        {
            _articleService = articleService;
            _articleDtoValidator = articleDtoValidator;
            _articleForCreationDtoValidator = articleForCreationDtoValidator;
            _articleForUpdateDtoValidator = articleForUpdateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticles();

            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById([FromRoute]int id)
        {
            if (!_articleService.IsArticleExist(id).Result)
                return NotFound("Article with id: " + id + " does not exist");

            var article = await _articleService.GetArticleById(id);

            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewArticle([FromBody] ArticleForCreationDto article)
        {
            var validationResult = _articleForCreationDtoValidator.Validate(article);

            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors.ToStringErrorMessages());
            }

            var newArticle = await _articleService.AddArticle(article);

            return StatusCode(201, new { Id = newArticle.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleById([FromRoute]int id) 
        {
            if(!_articleService.IsArticleExist(id).Result)
                return NotFound("Article with id: " + id + " does not exist");

            await _articleService.DeleteArticleById(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle([FromRoute]int id, [FromBody]ArticleForUpdateDto article) 
        {
            if (!_articleService.IsArticleExist(id).Result)
                return NotFound("Article with id:" + id + " does not exist");

            var validationResult = _articleForUpdateDtoValidator.Validate(article);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.ToStringErrorMessages());
            }

            await _articleService.UpdateArticle(id, article);
            
            return Ok();
        }
             
    }
}
