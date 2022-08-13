using System.ComponentModel.DataAnnotations;

namespace Main.API.DtoModels
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
