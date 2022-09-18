
namespace Main.API.Persistance
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublicationDate { get; set; }

        public User Owner { get; set; }
    }
}
