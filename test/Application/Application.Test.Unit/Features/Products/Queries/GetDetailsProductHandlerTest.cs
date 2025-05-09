﻿using Application.Contracts.Infrastructure.Repositories;
using Application.DTo.Products;
using Application.Features.Products.Queries;
using Domain.Test.Unit.Entities.Products;
using FluentAssertions;
using Moq;

namespace Application.Test.Unit.Features.Products.Queries;

public class GetDetailsProductHandlerTest
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly GetDetailsProductHandler _getDetailsProductHandler;
    private readonly ProductBuilder _productBuilder;
    public GetDetailsProductHandlerTest()
    {
        _productRepository = new Mock<IProductRepository>();
        _getDetailsProductHandler = new(_productRepository.Object);
        _productBuilder = new();
    }
    [Fact]
    public async Task Should_UseMethod()
    {
        //arrange
        GetDetailsProductRequest request = new GetDetailsProductRequest()
        {
            Id = 1
        };
        //act
        var result = await _getDetailsProductHandler.Handle(request, new());

        //assert
        _productRepository.Verify(p => p.GetByIdAsync(It.Is<int>(p => p == request.Id)));
    }
    [Fact]
    public async Task Should_ReturnNullIfNotFoundRecord()
    {
        //arrange
        GetDetailsProductRequest request = new GetDetailsProductRequest()
        {
            Id = 0
        };
        //act
        var result = await _getDetailsProductHandler.Handle(request, new());

        //assert
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().Contain("Record not found.");
    }
    [Fact]
    public async Task Should_ReturnDetailsProduct()
    {
        //arrange
        GetDetailsProductRequest request = new GetDetailsProductRequest()
        {
            Id = 1
        };
        var product = _productBuilder.BuildWithId(50);
        _productRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);


        //act
        var result = await _getDetailsProductHandler.Handle(request, new());
        var data = result.Data as DetailsProductDto;



        //assert
        result.Data.Should().NotBeNull();
        data.Should().BeEquivalentTo(product);
    }
}
