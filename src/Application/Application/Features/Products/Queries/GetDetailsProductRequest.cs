using Application.Contracts.Infrastructure.Repositories;
using Application.DTo.Products;
using MediatR;

namespace Application.Features.Products.Queries;

public class GetDetailsProductRequest : IRequest<DetailsProductDto?>
{
    public int Id { get; set; }
}
public class GetDetailsProductHandler : IRequestHandler<GetDetailsProductRequest, DetailsProductDto?>
{
    private readonly IProductRepository _productRepository;

    public GetDetailsProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<DetailsProductDto?> Handle(GetDetailsProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
            return null;

        return new DetailsProductDto(product.Id, product.Name, product.UnitPrice);
    }
}
