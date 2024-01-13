using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    [HttpGet("getCategory")]
    public IActionResult GetCategory()
    {
        try
        {
            using (var context = new MarketContext())
            {
                var categorys = context.Categories.ToList();
                return Ok(categorys);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("addCategory")]
    public IActionResult AddCategory(string name, string description)
    {
        try
        {
            using (var context = new MarketContext())
            {
                var existingCategory = context.Categories.FirstOrDefault(c => c.Name.ToLower()
                    .Equals(name.ToLower()));
                if (existingCategory == null)
                {
                    var newCategory = new Category
                    {
                        Name = name,
                        Description = description
                    };
                    context.Categories.Add(newCategory);
                    context.SaveChanges();

                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Category added successfully.",
                        Id = newCategory.Id
                    });
                }

                return StatusCode(409, new ApiResponse
                {
                    Success = false,
                    Message = "Category already exists."
                });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { Success = false, Message = ex.Message });
        }
    }

    [HttpDelete("deleteCategory/{id}")]
    public IActionResult DeleteCategory(int id)
    {
        try
        {
            using (var context = new MarketContext())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);

                if (category == null)
                    return NotFound(new ApiResponse { Success = false, Message = "Category not found." });

                var categoryId = category.Id; // Сохраняем ID до удаления
                context.Categories.Remove(category);
                context.SaveChanges();

                return Ok(new ApiResponse
                    { Success = true, Message = "Category deleted successfully.", Id = categoryId });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { Success = false, Message = ex.Message });
        }
    }
}