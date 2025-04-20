using Application.Commons.DTo;
using Application.Commons.Responses;
using Application.Contracts.Infrastructure.Repositories;
using Application.DTo.Products.Validations;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.Products.Commands;

public class AddProductRequest : IRequest<OperationResult>
{
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
}
public class AddProductHandler : IRequestHandler<AddProductRequest, OperationResult>
{
    private readonly IProductRepository _productRepository;

    public AddProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(AddProductRequest request, CancellationToken cancellationToken)
    {
        var newAddProductValidation = new AddProductValidation();
        var validationResult = newAddProductValidation.Validate(request).ToResult();
        if (!validationResult.IsSuccess)
            return validationResult;


        Product newProduct = new Product(request.Name, request.UnitPrice);

        await _productRepository.AddAsync(newProduct);
        await _productRepository.SaveChangesAsync();

        return OperationResult.Success(new IdResponse<int>() { Id = newProduct.Id });

    }
}
