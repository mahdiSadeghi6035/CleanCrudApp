using Application.Commons.Responses;
using Application.Contracts.Infrastructure.Repositories;
using Application.DTo.Products;
using MediatR;

namespace Application.Features.Products.Queries;

public class GetDetailsProductRequest : IRequest<OperationResult>
{
    public int Id { get; set; }
}
public class GetDetailsProductHandler : IRequestHandler<GetDetailsProductRequest, OperationResult>
{
    private readonly IProductRepository _productRepository;

    public GetDetailsProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(GetDetailsProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
            return OperationResult.Failure("Record not found.");

        return OperationResult.Success(new DetailsProductDto(product.Id, product.Name, product.UnitPrice));
    }
}
