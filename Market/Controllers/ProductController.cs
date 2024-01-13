using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet("getProducts")]
    public IActionResult GetProducts()
    {
        try
        {
            using (var context = new ProductContext())
            {
                var products = context.Products.ToList();
                return Ok(products);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("addProduct")]
    public IActionResult AddProduct(string name, string description, int categoryId, double cost)
    {
        try
        {
            using (var context = new ProductContext())
            {
                if (!context.Products.Any(p => p.Name.ToLower().Equals(name)))
                {
                    context.Add(new Product
                    {
                        Name = name,
                        Description = description,
                        Cost = cost,
                        CategoryId = categoryId
                    });
                    context.SaveChanges();
                    return Ok();
                }

                return StatusCode(409);
            }
        }
        catch
        {
            return StatusCode(500);
        }
    }
}