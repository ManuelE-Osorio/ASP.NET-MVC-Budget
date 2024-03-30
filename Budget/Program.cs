using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Budget.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Budget;

public class BudgetApp
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<BudgetContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BudgetConnectionString") ?? 
                throw new InvalidOperationException("Connection string 'BudgetContext' not found.")));

        var app = builder.Build();
        app.UseRequestLocalization( new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US")
            }
        );
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        using (var context = new BudgetContext( 
            app.Services.CreateScope().ServiceProvider.GetRequiredService<DbContextOptions<BudgetContext>>()))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Budget}/{action=Index}");

        app.Run();
    }
}


