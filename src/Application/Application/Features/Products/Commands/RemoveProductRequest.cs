using Application.Commons.Responses;
using Application.Contracts.Infrastructure.Repositories;
using MediatR;

namespace Application.Features.Products.Commands;

public class RemoveProductRequest : IRequest<OperationResult>
{
    public int Id { get; set; }
}
public class RemoveProductHandler : IRequestHandler<RemoveProductRequest, OperationResult>
{
    private readonly IProductRepository _productRepository;

    public RemoveProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
            return OperationResult.Failure("Record not found.");

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync();

        return OperationResult.Success();
    }
}
