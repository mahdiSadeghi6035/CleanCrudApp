using Application.DTo.Products;
using Domain.Entities.Products;

namespace Application.Contracts.Infrastructure.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task SaveChangesAsync();
    Task<Product?> GetByIdAsync(int id);
    void Remove(Product product);
    Task<List<ListProductDto>> GetAllAsync();
}
