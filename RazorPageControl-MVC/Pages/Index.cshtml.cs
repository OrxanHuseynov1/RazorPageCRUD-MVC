using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageControl_MVC.Context;
using RazorPageControl_MVC.Entities;
namespace RazorPageControl_MVC.Pages;

public class IndexModel(AppDbContext appDbContext) : PageModel
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public string? Message { get; set; }
    public string? Info { get; set; }
    public List<Product>? Products { get; set; }

    [BindProperty]
    public Product Product { get; set; }

    public void OnGet()
    {
        Products = [.. _appDbContext.Products];
        Message = $"Now date is {DateTime.Now.DayOfWeek}";
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        if (Product.Id == 0) 
        {
            await _appDbContext.Products.AddAsync(Product);
            Message = $"{Product.Name} added successfully";
        }
        else 
        {
            var existingProduct = await _appDbContext.Products.FindAsync(Product.Id);
            if (existingProduct is not null)
            {
                existingProduct.Name = Product.Name;
                existingProduct.Price = Product.Price;
                Message = $"{Product.Name} updated successfully";
            }
            else
            {
                Message = "Product not found for update.";
                return NotFound();
            }
        }

        await _appDbContext.SaveChangesAsync();
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostUpdateAsync(int id)
    {
        var product = await _appDbContext.Products.FindAsync(id);

        Product = product!;
        Products = [.. _appDbContext.Products];
        return Page(); 
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var product = await _appDbContext.Products.FindAsync(id);
        _appDbContext.Products.Remove(product!);
        await _appDbContext.SaveChangesAsync();
        Message = $"{product!.Name} deleted";
        return RedirectToPage("Index");
    }
}
