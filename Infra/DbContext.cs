using ErpCalciolari.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ErpCalciolari.Infra
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração global para converter DateTime para UTC
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                }
            }

            // Relacionamento Order -> Customer (mantendo por nome)
            modelBuilder.Entity<Order>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerName)
                .HasPrincipalKey(c => c.Name);

            // Definir OrderNumber como chave alternativa
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id); // Chave primária
            modelBuilder.Entity<Order>()
                .HasAlternateKey(o => o.OrderNumber); // Chave alternativa

            // Relacionamento Order -> OrderItems via OrderNumber
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderNumber)
                .HasPrincipalKey(o => o.OrderNumber);

            // Relacionamento OrderItem -> Product via ProductCode
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductCode)
                .HasPrincipalKey(p => p.Code);
        }
    }
}
