namespace Domain.Entities.Products;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }

    public Product(string? name, decimal unitPrice)
    {
        Name = name;
        UnitPrice = unitPrice;
    }
    public void Update(string? name, decimal? unitPrice)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (unitPrice.HasValue) UnitPrice = unitPrice.Value;
    }
}
