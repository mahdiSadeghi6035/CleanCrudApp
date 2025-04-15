using Domain.Entities.Products;

namespace Domain.Test.Unit.Entities.Products;

public class ProductBuilder
{
    int id = 50;
    string? name = "name";
    decimal unitPrice = 250;

    public Product Build() => new(name, unitPrice);

    public ProductBuilder SetName(string name)
    {
        this.name = name;
        return this;
    }

    public ProductBuilder SetUnitPrice(decimal unitPrice)
    {
        this.unitPrice = unitPrice;
        return this;
    }
}
