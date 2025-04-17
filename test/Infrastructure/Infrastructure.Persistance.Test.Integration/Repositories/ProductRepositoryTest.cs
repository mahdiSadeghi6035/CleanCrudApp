using Application.DTo.Products;
using Domain.Entities.Products;
using Domain.Test.Unit.Entities.Products;
using FluentAssertions;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Persistance.Test.Integration.SetupFixtures;

namespace Infrastructure.Persistance.Test.Integration.Repositories;

public class ProductRepositoryTest : IClassFixture<SandBoxDatabaseSetupFixture>
{
    private readonly SandBoxDatabaseSetupFixture _databaseSetupFixture;
    private readonly ProductRepository _productRepository;
    private readonly ProductBuilder _productBuilder;
    public ProductRepositoryTest(SandBoxDatabaseSetupFixture databaseSetupFixture)
    {
        _databaseSetupFixture = databaseSetupFixture;
        _productRepository = new ProductRepository(_databaseSetupFixture.Context);
        _productBuilder = new ProductBuilder();
    }

    private async Task<Product?> AddProductAsync()
    {
        var newProduct = _productBuilder.Build();

        await _productRepository.AddAsync(newProduct);
        await _productRepository.SaveChangesAsync();

        return newProduct;
    }
    [Fact]
    public async Task Should_AddNewProduct()
    {
        //arrange
        var newProduct = _productBuilder.Build();

        //act
        await _productRepository.AddAsync(newProduct);
        await _productRepository.SaveChangesAsync();

        //assert
        newProduct.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Should_GetProductById()
    {
        //arrange
        var newProduct = await AddProductAsync();

        //act
        var result = await _productRepository.GetByIdAsync(newProduct.Id);

        //assert
        result.Should().NotBeNull();
        result.Id.Should().Be(newProduct.Id);
    }
    [Fact]
    public async Task Should_GetProductByIdNotFoundRecordReturnNull()
    {
        //act
        var result = await _productRepository.GetByIdAsync(0);

        //assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Should_RemoveProduct()
    {
        //arrange
        var newProduct = await AddProductAsync();

        //act
        _productRepository.Remove(newProduct);
        await _productRepository.SaveChangesAsync();

        var getProduct = await _productRepository.GetByIdAsync(newProduct.Id);

        //assert
        getProduct.Should().BeNull();
    }

    [Fact]
    public async Task Should_GetAllProduct()
    {
        //arrange
        var newProduct = await AddProductAsync();

        //act
        var result = await _productRepository.GetAllAsync();

        //assert
        result.Should().HaveCountGreaterThan(0);
        result.Should().BeOfType<List<ListProductDto>>();
        result.Select(p => p.Id).Should().Contain(newProduct.Id);
    }
}
