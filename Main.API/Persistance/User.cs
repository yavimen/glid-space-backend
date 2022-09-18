using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Main.API.Persistance
{
    public class User: IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
