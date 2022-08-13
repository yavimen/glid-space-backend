using System.ComponentModel.DataAnnotations;

namespace Main.API.DtoModels
{
    public class ArticleForCreationDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Publication date is required")]
        public DateTime PublicationDate { get; set; }
    }
}
