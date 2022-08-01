using System.ComponentModel.DataAnnotations;

namespace Main.API.DtoModels
{
    public class ArticleForCreationDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title;
        [Required(ErrorMessage = "Content is required")]
        public string Content;
        [Required(ErrorMessage = "Publication date is required")]
        public DateTime PublicationDate;
    }
}
