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
            using (var context = new MarketContext())
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
    public IActionResult AddProduct(string name, string description, int categoryId, double price)
    {
        try
        {
            using (var context = new MarketContext())
            {
                var existingProduct = context.Products.FirstOrDefault(p => p.Name.ToLower()
                    .Equals(name.ToLower()));
                if (existingProduct == null)
                {
                    var newProduct = new Product
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        CategoryId = categoryId
                    };
                    context.Products.Add(newProduct);
                    context.SaveChanges();

                    return Ok(new ApiResponse
                        { Success = true, Message = "Product added successfully.", Id = newProduct.Id });
                }

                return StatusCode(409, new ApiResponse { Success = false, Message = "Product already exists." });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { Success = false, Message = ex.Message });
        }
    }

    [HttpDelete("deleteProduct/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            using (var context = new MarketContext())
            {
                var product = context.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                    return NotFound(new ApiResponse { Success = false, Message = "Product not found." });

                var productId = product.Id; // Сохраняем ID до удаления
                context.Products.Remove(product);
                context.SaveChanges();

                return Ok(new ApiResponse
                    { Success = true, Message = "Product deleted successfully.", Id = productId });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { Success = false, Message = ex.Message });
        }
    }

    [HttpPut("updateProductPrice/{id}")]
    public IActionResult UpdateProductPrice(int id, double newPrice)
    {
        try
        {
            using (var context = new MarketContext())
            {
                var product = context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                    return NotFound(new ApiResponse { Success = false, Message = "Product not found." });

                product.Price = newPrice; // Обновление цены продукта
                context.SaveChanges();
                return Ok(new ApiResponse
                    { Success = true, Message = "Product price updated successfully.", Id = product.Id });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { Success = false, Message = ex.Message });
        }
    }
}