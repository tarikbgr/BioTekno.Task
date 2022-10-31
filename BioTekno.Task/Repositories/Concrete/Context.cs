using System.Drawing;
using BioTekno.Task.Models.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BioTekno.Task.Repositories.Concrete;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {

    }


    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Product>().HasData(
    //         new Faker<Product>()
    //        .RuleFor(x => x.Id, a => a.Random.Int(2,2000))
    //        .RuleFor(x => x.Category, a => a.Commerce.ProductName())
    //        .RuleFor(x => x.UpdateTime, a => a.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now))
    //        .RuleFor(x => x.CreateTime, a => a.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now))
    //        .RuleFor(x => x.Description, a => a.Name.FullName())           
    //        .RuleFor(x => x.Unit, a => a.Random.Int(5, 10))
    //        .RuleFor(x => x.UnitPrice, a => a.Random.Double())
    //        .Generate(1000)
    // );
    //}


}