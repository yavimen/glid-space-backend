using System.ComponentModel.DataAnnotations;

namespace Main.API.DtoModels
{
    public class ArticleDto
    {
        [Required()]
        public int Id;
        [Required()]
        public string Title;
        [Required()]
        public string Content;
        [Required()]
        public DateTime PublicationDate;
    }
}
