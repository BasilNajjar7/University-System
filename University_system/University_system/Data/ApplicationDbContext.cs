using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using University_system.Model;

namespace University_system.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(b => b.Email).IsUnique();

            builder.Entity<MaterialStudent>()
                .HasKey(k => new { k.StudentId, k.MaterialId });

            builder.Entity<MaterialStudent>()
                .HasOne(o => o.Student)
                .WithMany(o => o.MaterialStudents)
                .HasForeignKey(o => o.StudentId);

            builder.Entity<MaterialStudent>()
                .HasOne(o => o.Material)
                .WithMany(o => o.MaterialStudents)
                .HasForeignKey(o => o.MaterialId);

            base.OnModelCreating(builder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MaterialStudent> MaterialStudents { get; set; }
    }
}