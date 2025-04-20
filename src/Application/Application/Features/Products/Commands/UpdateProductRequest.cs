
using Application.Commons.Responses;
using Application.Contracts.Infrastructure.Repositories;
using Application.DTo.Products.Validations;
using MediatR;

namespace Application.Features.Products.Commands;

public class UpdateProductRequest : IRequest<OperationResult>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
}
public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, OperationResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var newUpdateProductValidation = new UpdateProductValidation();
        var validationResult = newUpdateProductValidation.Validate(request).ToResult();
        if (!validationResult.IsSuccess)
            return validationResult;
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
            return OperationResult.Failure("Record not found.");

        product.Update(request.Name, request.UnitPrice);
        await _productRepository.SaveChangesAsync();

        return OperationResult.Success();

    }
}
