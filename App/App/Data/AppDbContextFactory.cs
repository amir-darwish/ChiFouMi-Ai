using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using App.Data;

namespace App.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=chifoumi_db;Username=admin;Password=admin");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}