using Domain.Entities.Products;
using FluentAssertions;

namespace Domain.Test.Unit.Entities.Products;

public class ProductTest
{
    private readonly ProductBuilder _productBuilder;
    public ProductTest()
    {
        _productBuilder = new();
    }

    [Fact]
    public void Should_Constructor_SetValue()
    {
        //arrange
        string? name = "name";
        decimal unitPrice = 250;

        //act
        Product newProduct = new(name, unitPrice);

        //assert
        newProduct.Name.Should().Be(name);
        newProduct.UnitPrice.Should().Be(unitPrice);
    }

    [Fact]
    public void Should_Update_SetValue()
    {
        //arrange
        var newProduct = _productBuilder.Build();

        string? name = "new name";
        decimal unitPrice = 545;

        //act
        newProduct.Update(name, unitPrice);

        //assert
        newProduct.Name.Should().Be(name);
        newProduct.UnitPrice.Should().Be(unitPrice);
    }
}
