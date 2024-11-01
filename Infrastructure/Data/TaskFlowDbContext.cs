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
            // Configuracinn de las entidades y sus relaciones

            // Configuracinn de User y herencia con Admin y Client
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.State).HasDefaultValue(true);
            });

            // Configuracinn de la herencia (TPH - Table Per Hierarchy)
            modelBuilder.Entity<Admin>().HasBaseType<User>();
            modelBuilder.Entity<Client>().HasBaseType<User>();

            // Configuracinn de Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProjectName).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.State).HasDefaultValue(true);

                // Relacinn con Admin (uno a muchos)
                entity.HasOne(p => p.Admin)
                      .WithMany(a => a.CreatedProjects)
                      .HasForeignKey(p => p.AdminId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relacion con Client (uno a muchos)
                entity.HasOne(p => p.Client)
                      .WithMany(c => c.AssignedProjects)
                      .HasForeignKey(p => p.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relacion con Comments
                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Project)
                      .HasForeignKey(c => c.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuracion de Comment
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

            var admin1 = new Admin
            {
                Id = 1,
                Name = "Admin",
                Email = "tanoni@gmail.com",
                UserName = "Tano",
                Password = BCrypt.Net.BCrypt.HashPassword("Clave123!"), // Encriptacion de la contraseña
                State = true
            };

            var client1 = new Client
            {
                Id = 2,
                Name = "Carlos",
                Email = "carlos@hotmail.com",
                UserName = "Carlos",
                Password = BCrypt.Net.BCrypt.HashPassword("Clave123!"),
                State = true
            };

            // Agregar los datos al modelo
            modelBuilder.Entity<Admin>().HasData(admin1);
            modelBuilder.Entity<Client>().HasData(client1);
        }
    }
}
