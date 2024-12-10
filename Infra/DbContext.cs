using ErpCalciolari.Models;
using Microsoft.EntityFrameworkCore;

namespace ErpCalciolari.Infra
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
