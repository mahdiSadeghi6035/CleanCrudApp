using Application.Commons.DTo;
using Application.Commons.Responses;
using Application.DTo.Products;
using Application.Features.Products.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Api.Test.Integration.Api;

public class ProductsTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;
    private string _url = "/api/v1/products/";
    public ProductsTest(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _httpClient = _webApplicationFactory.CreateClient();
    }
    private async Task<IdResponse<int>> AddNewProductAsync()
    {
        AddProductRequest request = new()
        {
            Name = "name",
            UnitPrice = 450
        };

        var apiResult = await _httpClient.PostAsJsonAsync(_url, request);
        var result = await apiResult.Content.ReadFromJsonAsync<OperationResult>();
        var data = JsonConvert.DeserializeObject<IdResponse<int>>(result.Data.ToString());

        return data;
    }
    [Fact]
    public async Task Should_AddProduct_ValidationErrors()
    {
        //arrange
        AddProductRequest request = new();

        //act
        var apiResult = await _httpClient.PostAsJsonAsync(_url, request);
        var result = await apiResult.Content.ReadFromJsonAsync<OperationResult>();

        //assert
        apiResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().BeEquivalentTo(new string[] { "Name is required", "UnitPrice is required" });
    }
    [Fact]
    public async Task Should_AddProduct_Success()
    {
        //arrange
        AddProductRequest request = new()
        {
            Name = "name",
            UnitPrice = 450
        };

        //act
        var apiResult = await _httpClient.PostAsJsonAsync(_url, request);
        var result = await apiResult.Content.ReadFromJsonAsync<OperationResult>();
        var data = JsonConvert.DeserializeObject<IdResponse<int>>(result.Data.ToString());

        //assert
        apiResult.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Messages.Should().BeEmpty();
        data.Id.Should().BeGreaterThan(0);
    }
    [Fact]
    public async Task Should_RemoveProduct_NotFoundError()
    {
        //arrange
        _url = _url + "0";
        //act
        var apiResult = await _httpClient.DeleteAsync(_url);
        var result = await apiResult.Content.ReadFromJsonAsync<OperationResult>();

        //assert
        apiResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().Contain("Record not found.");
    }

    [Fact]
    public async Task Should_RemoveProduct_Success()
    {
        //arrange
        var productId = await AddNewProductAsync();
        _url = _url + $"{productId.Id}";

        //act
        var apiResult = await _httpClient.DeleteAsync(_url);
        var result = await apiResult.Content.ReadFromJsonAsync<OperationResult>();

        //assert
        apiResult.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Messages.Should().BeEmpty();
    }
    [Fact]
    public async Task Should_UpdateProduct_ValidationError()
    {
        //arrange
        UpdateProductRequest request = new();

        //act
        var apiResult = await _httpClient.PutAsJsonAsync(_url, request);
        var result = await apiResult.Content.ReadFromJsonAsync<OperationResult>();

        //assert
        apiResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().BeEquivalentTo(new string[] { "Name is required", "UnitPrice is required", "Id is required" });
    }
    [Fact]
    public async Task Should_UpdateProduct_NotFoundError()
    {
        //arrange
        UpdateProductRequest request = new UpdateProductRequest() { Id = 88888, UnitPrice = 500, Name = "name" };

        //act
        var apiResult = await _httpClient.PutAsJsonAsync(_url, request);
        var result = await apiResult.Content.ReadFromJsonAsync<OperationResult>();

        //assert
        apiResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccess.Should().BeFalse();
        result.Messages.Should().Contain("Record not found.");
    }

    [Fact]
    public async Task Should_UpdateProduct_Success()
    {
        //arrange
        var productId = await AddNewProductAsync();
        UpdateProductRequest request = new UpdateProductRequest() { Id = productId.Id, UnitPrice = 500, Name = "name" };

        //act
        var apiResult = await _httpClient.PutAsJsonAsync(_url, request);
        var result = await apiResult.Content.ReadFromJsonAsync<OperationResult>();

        //assert
        apiResult.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Messages.Should().BeEmpty();
    }
    [Fact]
    public async Task Should_GetDetailsProduct_NotFound()
    {
        //arrange
        _url = _url + "0";

        //act
        var apiResult = await _httpClient.GetAsync(_url);

        //arrange
        apiResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task Should_GetDetailsProduct()
    {
        //arrange
        var productId = await AddNewProductAsync();
        _url = _url + $"{productId.Id}";

        //act
        var apiResult = await _httpClient.GetAsync(_url);
        var result = await apiResult.Content.ReadFromJsonAsync<DetailsProductDto>();

        //arrange
        apiResult.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Id.Should().Be(productId.Id);
    }
    [Fact]
    public async Task Should_GetListProducts()
    {

        //arrange
        var productId = await AddNewProductAsync();

        //act
        var apiResult = await _httpClient.GetAsync(_url);
        var result = await apiResult.Content.ReadFromJsonAsync<List<ListProductDto>>();

        //assert
        result.Should().HaveCountGreaterThan(0);
        result.Select(p => p.Id).Should().Contain(productId.Id);
    }
}
