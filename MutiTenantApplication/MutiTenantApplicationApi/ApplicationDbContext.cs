using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace MutiTenantApplicationApi
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(e => e.TenantId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => new { e.TenantId, e.UserName }).IsUnique();
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.Tenant)
                      .WithMany(t => t.Users)
                      .HasForeignKey(e => e.TenantId);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
                entity.HasOne(e => e.Tenant)
                      .WithMany(t => t.Posts)
                      .HasForeignKey(e => e.TenantId);
            });
        }

        public override int SaveChanges()
        {
            AddTenantId();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTenantId();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTenantId()
        {
            var tenantId = _httpContextAccessor.HttpContext.Items["TenantId"]?.ToString();
            if (!string.IsNullOrEmpty(tenantId) && int.TryParse(tenantId, out var parsedTenantId))
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.Entity is ITenantEntity tenantEntity && entry.State == EntityState.Added)
                    {
                        tenantEntity.TenantId = parsedTenantId;
                    }
                }
            }
        }
    }

    public interface ITenantEntity
    {
        int TenantId { get; set; }
    }

    public class Tenant
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

    public class User : ITenantEntity
    {
        public int UserId { get; set; }
        public int TenantId { get; set; } // Change to int to match TenantId in Tenant entity
        public string UserName { get; set; }
        public Tenant Tenant { get; set; }
    }

    public class Post : ITenantEntity
    {
        public int PostId { get; set; }
        public int TenantId { get; set; } // Change to int to match TenantId in Tenant entity
        public string Title { get; set; }
        public string Content { get; set; }
        public Tenant Tenant { get; set; }
    }
}