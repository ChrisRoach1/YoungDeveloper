using YoungDeveloper.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace YoungDeveloper.Application.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

    public virtual DbSet<User> Users { set; get; }
    public virtual DbSet<Role> Roles { set; get; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<ProjectRequest> ProjectRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // it should be placed here, otherwise it will rewrite the following settings!
        base.OnModelCreating(builder);

        // Custom application mappings
        builder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(450).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();

            entity.HasMany(e => e.Projects).WithOne(e => e.User).HasForeignKey(e => e.CreatedById);
            entity.HasMany(e => e.ProjectRequests).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        });

        builder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        builder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.RoleId);
            entity.Property(e => e.UserId);
            entity.Property(e => e.RoleId);
            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
            entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
        });

        builder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(2500).IsRequired();
            entity.HasOne(e => e.User).WithMany(e => e.Projects).HasForeignKey(e => e.CreatedById);
            entity.HasMany(e => e.ProjectRequests).WithOne(e => e.Project).HasForeignKey(e => e.ProjectId);
        });

        builder.Entity<Role>().HasData(
            new Role { Id = 1, Name = CustomRoles.User },
            new Role { Id = 2, Name = CustomRoles.Admin }
        );
    }
}
