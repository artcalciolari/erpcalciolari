using ErpCalciolari.Models;
using Microsoft.EntityFrameworkCore;

namespace ErpCalciolari.Infra
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
    {
    }
}
