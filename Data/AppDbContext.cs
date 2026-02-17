using Microsoft.EntityFrameworkCore;

namespace مشروع_قبل_الشغل.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        { 
        
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UsersPermation>().ToTable("UserPermission").HasKey(x=>new{x.userId,x.PermissionId}); 
        }
        
    }
}
