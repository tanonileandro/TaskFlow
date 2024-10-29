using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaskFlowDbContext : DbContext
    {
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
        {
        }

        // DbSets para nuestras entidades
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de las entidades y sus relaciones

            // Configuración de User y herencia con Admin y Client
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.State).HasDefaultValue(true);
            });

            // Configuración de la herencia (TPH - Table Per Hierarchy)
            modelBuilder.Entity<Admin>().HasBaseType<User>();
            modelBuilder.Entity<Client>().HasBaseType<User>();

            // Configuración de Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProjectName).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.State).HasDefaultValue(true);

                // Relación con Admin (uno a muchos)
                entity.HasOne(p => p.Admin)
                      .WithMany(a => a.CreatedProjects)
                      .HasForeignKey(p => p.AdminId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Client (uno a muchos)
                entity.HasOne(p => p.Client)
                      .WithMany(c => c.AssignedProjects)
                      .HasForeignKey(p => p.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Comments
                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Project)
                      .HasForeignKey(c => c.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();

                // Relación con User (CreatedBy)
                entity.HasOne(c => c.CreatedBy)
                      .WithMany()
                      .HasForeignKey(c => c.CreatedById)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Project
                entity.HasOne(c => c.Project)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
