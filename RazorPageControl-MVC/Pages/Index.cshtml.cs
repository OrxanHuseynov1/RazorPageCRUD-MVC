using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageControl_MVC.Context;
using RazorPageControl_MVC.Entities;
namespace ViewComponentLesson.Pages;

public class IndexModel(AppDbContext appDbContext) : PageModel
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public string? Message { get; set; }
    public string? Info { get; set; }
    public List<Product>? Products { get; set; }


    public void OnGet()
    {
        Products = [.. _appDbContext.Products];
        Message = $"Now date is {DateTime.Now.DayOfWeek}";
    }

    [BindProperty]
    public Product Product { get; set; }

    public IActionResult OnPost()
    {
        _appDbContext.Products.Add(Product);
        _appDbContext.SaveChanges();
        Message = $"{Product.Name} added succesfully";
        return RedirectToPage("Index");
    }
}