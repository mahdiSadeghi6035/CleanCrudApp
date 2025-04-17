
using Application.Contracts.Infrastructure.Repositories;
using Application.Features.Products.Commands;
using Domain.Entities.Products;
using FluentAssertions;
using Moq;

namespace Application.Test.Unit.Features.Products.Commands;

public class AddProductHandlerTest
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly AddProductHandler _addProductHandler;
    public AddProductHandlerTest()
    {
        _productRepository = new Mock<IProductRepository>();
        _addProductHandler = new(_productRepository.Object);
    }
    [Fact]
    public async Task Should_UseMethod()
    {
        //arrange
        AddProductRequest request = new AddProductRequest() { Name = "name", UnitPrice = 150 };

        //act
        var result = await _addProductHandler.Handle(request, new());

        //assert
        _productRepository.Verify(p => p.AddAsync(It.IsAny<Product>()));
        _productRepository.Verify(p => p.SaveChangesAsync());
    }
    [Fact]
    public async Task Should_ValidationError()
    {
        //arrange
        AddProductRequest request = new AddProductRequest();

        //act
        var result = await _addProductHandler.Handle(request, new());

        //assert
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().BeEquivalentTo(new string[] { "Name is required", "UnitPrice is required" });
    }
    [Fact]
    public async Task Should_SuccessAddNewProduct()
    {
        //arrange
        AddProductRequest request = new AddProductRequest() { Name = "name", UnitPrice = 150 };

        //act
        var result = await _addProductHandler.Handle(request, new());

        //assert
        result.IsSuccess.Should().BeTrue();
        result.Messages.Should().BeEmpty();
    }
}
