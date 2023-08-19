using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ASP_Net_Core_example.Models
{
    public class MyAppContext : DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }

        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("UserInfos");
        }
    }
}
