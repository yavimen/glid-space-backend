using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.API.Persistance
{
    [Table("Articles")]
    public class Article
    {
        public int Id;

        public string Title;

        public string Content;

        public DateTime PublicationDate;
    }
}
