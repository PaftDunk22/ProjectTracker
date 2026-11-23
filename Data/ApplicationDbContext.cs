using Microsoft.EntityFrameworkCore;
using Tracker.Models;

namespace Tracker.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Project> Projects { get; set; }

        public DbSet<Issue> Issues { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
    }
}
