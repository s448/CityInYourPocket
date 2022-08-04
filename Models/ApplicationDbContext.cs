using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Banner> banners { get; set; }
        public DbSet<Charity> charities { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<Market> markets { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<Service> services{ get; set; }
        public DbSet<Shop> shops { get; set; }

    }
}
