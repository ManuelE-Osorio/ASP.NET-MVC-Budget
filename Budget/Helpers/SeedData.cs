using System;
using System.Linq;
using Budget.Models;
using Microsoft.Net.Http.Headers;

namespace Budget.Helpers;

public class SeedData
{
    public static void SeedCategories(BudgetContext context)
    {
        if(context.Categories.Any())
            return;
        
        context.Categories.AddRange([
            new Category {
                Name = "Groceries"
            },
            new Category {
                Name = "Utilities"
            },
            new Category {
                Name = "Housing"
            },
            new Category {
                Name = "Insurance"
            },
            new Category {
                Name = "HealthCare"
            },
            new Category {
                Name = "Entertainment"
            },
            new Category {
                Name = "Clothing"
            },
            new Category {
                Name = "Pets"
            },
            new Category {
                Name = "Education"
            }
        ]);
        context.SaveChanges();
    }

    public static void SeedTransactions(BudgetContext context)
    {
        if(context.Transactions.Any())
            return;

        context.Transactions.AddRange([
            new Transaction {
                Name = "Electricity",
                Description = "Electricity expenses of April",
                Date = new DateTime(2024, 4, 1, 16, 30, 0),
                Amount = 250,
                Category = context.Categories.FirstOrDefault( p => p.Name == "Utilities")!
            },
            new Transaction {
                Name = "Rent",
                Description = "Rent of April",
                Date = new DateTime(2024, 04, 01, 14, 00, 0),
                Amount = 2800,
                Category = context.Categories.FirstOrDefault( p => p.Name == "Housing")!
            },
            new Transaction {
                Name = ,
                Description = ,
                Date = ,
                Amount = ,
                Category = context.Categories.FirstOrDefault( p => p.Name == )!
            },
            new Transaction {
                Name = ,
                Description = ,
                Date = ,
                Amount = ,
                Category = context.Categories.FirstOrDefault( p => p.Name == )!
            },
            new Transaction {
                Name = ,
                Description = ,
                Date = ,
                Amount = ,
                Category = context.Categories.FirstOrDefault( p => p.Name == )!
            },
            new Transaction {
                Name = ,
                Description = ,
                Date = ,
                Amount = ,
                Category = context.Categories.FirstOrDefault( p => p.Name == )!
            },
        ]);

    }
}