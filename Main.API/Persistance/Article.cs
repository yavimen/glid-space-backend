using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.API.Persistance
{
    public class Article
    {
        public int Id;

        public string Title;

        public string Content;

        public DateTime PublicationDate;
    }
}
