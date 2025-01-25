using Microsoft.EntityFrameworkCore;
using RazorPageControl_MVC.Entities;
using System.Collections.Generic;

namespace RazorPageControl_MVC.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

}
