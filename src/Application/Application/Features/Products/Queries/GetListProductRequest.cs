using Application.Contracts.Infrastructure.Repositories;
using Application.DTo.Products;
using MediatR;

namespace Application.Features.Products.Queries;

public class GetListProductRequest : IRequest<List<ListProductDto>>
{
}
public class GetListProductHandler : IRequestHandler<GetListProductRequest, List<ListProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetListProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ListProductDto>> Handle(GetListProductRequest request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync();
    }
}
