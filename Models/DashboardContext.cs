using Microsoft.EntityFrameworkCore;
 
namespace user_dashboard.Models
{
    public class DashboardContext : DbContext
    {
        public DashboardContext(DbContextOptions<DashboardContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<Comment> comments { get; set; }
        
        
        
        
    }
}
