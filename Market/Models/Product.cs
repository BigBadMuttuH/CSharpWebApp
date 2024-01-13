namespace Market.Models;

public class Product : BaseModel
{
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public virtual List<ProductStorage> ProductStorages { get; set; } = new();
}