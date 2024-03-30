using Microsoft.EntityFrameworkCore;
using Budget.Models;

namespace Budget.Models;

public class BudgetContext(DbContextOptions<BudgetContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>( p => 
            {
                p.HasOne( p => p.Category)
                    .WithMany( p => p.Transactions)
                    .IsRequired();
                p.Property( p => p.Date).IsRequired();
            });
    }
}

