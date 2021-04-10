using Microsoft.EntityFrameworkCore;
namespace WebProjectll.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithMany(p => p.Users)
                .UsingEntity<ProjectUser>(
                    pu => pu.HasOne(prop => prop.Project)
                    .WithMany()
                    .HasForeignKey(prop => prop.Projectsid),
                    pu => pu.HasOne(prop => prop.User)
                    .WithMany()
                    .HasForeignKey(prop => prop.Usersid)
                );
        } 
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeReport> TimeReports { get; set; }
        public DbSet<ProjectUser> Project_user { get; set; }
    }
}