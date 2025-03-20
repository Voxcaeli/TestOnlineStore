using Microsoft.EntityFrameworkCore;
using TestOnlineStore.Domain;

namespace TestOnlineStore.Persistence;

public class TestOnlineStoreDBContext(DbContextOptions<TestOnlineStoreDBContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
