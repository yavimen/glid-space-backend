using System.ComponentModel.DataAnnotations;

namespace Main.API.DtoModels
{
    public class ArticleForUpdateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
