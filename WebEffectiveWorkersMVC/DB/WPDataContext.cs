using Microsoft.EntityFrameworkCore;
using WebEffectiveWorkersMVC.Models;

namespace WebEffectiveWorkersMVC.DB
{
    public partial class WPDataContext : DbContext
    {
        public WPDataContext()
        {
        }

        public WPDataContext(DbContextOptions<WPDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");
        }
    }
}
