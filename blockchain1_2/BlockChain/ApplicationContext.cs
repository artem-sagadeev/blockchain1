using Microsoft.EntityFrameworkCore;

namespace BlockChain;

public class ApplicationContext : DbContext
{
    public DbSet<Block> Blocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=blockchain_db;Username=postgres;Password=qweasd123");
    }
}