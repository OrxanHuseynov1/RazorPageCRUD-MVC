using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageControl_MVC.Context;
using RazorPageControl_MVC.Entities;
namespace RazorPageControl_MVC.Pages;

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

    public async Task<IActionResult> OnPostAsync()
    {
        var checkProduct = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == Product.Id);

        if(checkProduct is null)
        {
            await _appDbContext.Products.AddAsync(Product);
            await _appDbContext.SaveChangesAsync();
            Message = $"{Product.Name} added successfully";
            return RedirectToPage("Index");
        }
        else
        {
            _appDbContext.Products.Update(checkProduct);
            await _appDbContext.SaveChangesAsync();
            Message = $"{Product.Name} Update";
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostUpdateAsync(int id)
    {
        var oldProduct = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (oldProduct is null) { Message = "Product Not Found"; return NotFound(); }
        Product = oldProduct;
        Products = [.. _appDbContext.Products];
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null) { Message = "Product Not Found"; return NotFound(); }

        _appDbContext.Products.Remove(product);
        await _appDbContext.SaveChangesAsync();
        Message = $"{Product.Name} deleted";
        return RedirectToPage("Index");
    }


}