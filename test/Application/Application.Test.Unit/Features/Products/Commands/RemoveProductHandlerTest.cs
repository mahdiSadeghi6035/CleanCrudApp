using Application.Contracts.Infrastructure.Repositories;
using Application.Features.Products.Commands;
using Domain.Entities.Products;
using Domain.Test.Unit.Entities.Products;
using FluentAssertions;
using Moq;

namespace Application.Test.Unit.Features.Products.Commands;

public class RemoveProductHandlerTest
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly RemoveProductHandler _removeProductHandler;
    private readonly ProductBuilder _productBuilder;
    public RemoveProductHandlerTest()
    {
        _productRepository = new Mock<IProductRepository>();
        _removeProductHandler = new(_productRepository.Object);
        _productBuilder = new ProductBuilder();
    }
    [Fact]
    public async Task Should_UseMethod()
    {
        //arrange
        RemoveProductRequest request = new()
        {
            Id = 1
        };
        _productRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_productBuilder.Build());

        //act
        var result = await _removeProductHandler.Handle(request, new());

        //assert
        _productRepository.Verify(p => p.Remove(It.IsAny<Product>()));
        _productRepository.Verify(p => p.GetByIdAsync(It.IsAny<int>()));
        _productRepository.Verify(p => p.SaveChangesAsync());
    }
    [Fact]
    public async Task Should_RecordNotFoundError()
    {
        //arrange
        RemoveProductRequest request = new()
        {
            Id = 0
        };

        //act
        var result = await _removeProductHandler.Handle(request, new());

        //assert
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().Contain("Record not found.");
    }
    [Fact]
    public async Task Should_SuccessRemoveProduct()
    {
        //arrange
        RemoveProductRequest request = new()
        {
            Id = 1
        };
        _productRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_productBuilder.Build());
        //act
        var result = await _removeProductHandler.Handle(request, new());

        //assert
        result.IsSuccess.Should().BeTrue();
        result.Messages.Should().BeEmpty();
    }

}
