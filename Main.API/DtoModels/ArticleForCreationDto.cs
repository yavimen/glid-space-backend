using System.ComponentModel.DataAnnotations;

namespace Main.API.DtoModels
{
    public class ArticleForCreationDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
