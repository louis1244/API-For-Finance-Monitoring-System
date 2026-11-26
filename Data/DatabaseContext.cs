using ApplicationAPIDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAPIDemo.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Assets> Assets { get; set; }
        public DbSet<Dependents> Dependents { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Finances> Finance { get; set; }
    }
}
