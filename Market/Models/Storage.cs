namespace Market.Models;

public class Storage : BaseModel
{
    public Product Product { get; set; }
    public int ProductId { get; set; }

    public virtual List<ProductStorage> ProductStorages { get; set; } = new();
}