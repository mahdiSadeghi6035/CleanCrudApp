
using Application.Contracts.Infrastructure.Repositories;
using Application.Features.Products.Commands;
using Domain.Test.Unit.Entities.Products;
using FluentAssertions;
using Moq;

namespace Application.Test.Unit.Features.Products.Commands;

public class UpdateProductHandlerTest
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly UpdateProductHandler _updateProductHandler;
    private readonly ProductBuilder _productBuilder;
    public UpdateProductHandlerTest()
    {
        _productRepository = new Mock<IProductRepository>();
        _updateProductHandler = new(_productRepository.Object);
        _productBuilder = new();
    }
    [Fact]
    public async Task Should_UseMethod()
    {
        //arrange
        UpdateProductRequest request = new() { Id = 1, Name = "name", UnitPrice = 500 };
        _productRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_productBuilder.Build());


        //act
        var result = await _updateProductHandler.Handle(request, new());

        //assert
        _productRepository.Verify(s => s.GetByIdAsync(It.IsAny<int>()));
        _productRepository.Verify(s => s.SaveChangesAsync());

    }
    [Fact]
    public async Task Should_ValidationError()
    {
        //arrange
        UpdateProductRequest request = new();

        //act
        var result = await _updateProductHandler.Handle(request, new());

        //assert
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().BeEquivalentTo(new string[] { "Name is required", "UnitPrice is required", "Id is required" });
    }
    [Fact]
    public async Task Should_RecordNotFoundError()
    {
        //arrange
        UpdateProductRequest request = new() { Id = 1, Name = "name", UnitPrice = 500 };

        //act
        var result = await _updateProductHandler.Handle(request, new());

        //assert
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().Contain("Record not found.");
    }
    [Fact]
    public async Task Should_SuccessUpdateProduct()
    {
        //arrange
        UpdateProductRequest request = new() { Id = 1, Name = "name", UnitPrice = 500 };
        _productRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_productBuilder.Build());


        //act
        var result = await _updateProductHandler.Handle(request, new());

        //assert
        result.IsSuccess.Should().BeTrue();
        result.Messages.Should().BeEmpty();
    }
   
}
