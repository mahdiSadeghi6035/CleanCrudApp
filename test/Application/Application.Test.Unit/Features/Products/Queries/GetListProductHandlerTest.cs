using Application.Contracts.Infrastructure.Repositories;
using Application.Features.Products.Queries;
using Moq;

namespace Application.Test.Unit.Features.Products.Queries;

public class GetListProductHandlerTest
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly GetListProductHandler _getListProductHandler;
    public GetListProductHandlerTest()
    {
        _productRepository = new Mock<IProductRepository>();
        _getListProductHandler = new(_productRepository.Object);
    }
    [Fact]
    public async Task Should_UseMethod()
    {
        //arrange
        GetListProductRequest request = new();

        //act
        var result = await _getListProductHandler.Handle(request, new());
        //assert
        _productRepository.Verify(s => s.GetAllAsync());
    }
}
