using Domain.Entities.Products;

namespace Domain.Test.Unit.Entities.Products;

public class ProductBuilder
{
    string? name = "name";
    decimal unitPrice = 250;

    public Product Build() => new(name, unitPrice);
    public Product BuildWithId(int id) => new(name, unitPrice) { Id = id };

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
