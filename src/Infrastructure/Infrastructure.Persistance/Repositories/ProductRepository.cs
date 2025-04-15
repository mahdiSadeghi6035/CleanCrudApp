using Application.Contracts.Infrastructure.Repositories;
using Application.DTo.Products;
using Domain.Entities.Products;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Product product) => await _dbContext.Products.AddAsync(product);
    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    public async Task<Product?> GetByIdAsync(int id) => await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
    public void Remove(Product product) => _dbContext.Products.Remove(product);

    public async Task<List<ListProductDto>> GetAllAsync() =>
        await _dbContext.Products.Select(p => new ListProductDto(p.Id, p.Name!, p.UnitPrice)).ToListAsync();

}
