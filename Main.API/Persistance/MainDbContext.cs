using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Main.API.Configurations;

namespace Main.API.Persistance
{
    public class MainDbContext : IdentityDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        { }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<Article>()
                .HasOne(a => a.Owner)
                .WithMany(o => o.Articles);

            builder.ApplyConfiguration(new RoleConfiguration());
          
        }
    }
}