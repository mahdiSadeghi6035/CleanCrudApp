using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistance.Test.Integration.SetupFixtures;

public class SandBoxDatabaseSetupFixture : IDisposable
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=CleanBase;Persist Security Info=True; User ID= sa; Password=13846035;Encrypt=False";
    public ProductDbContext Context { get; set; }
    private IDbContextTransaction _transaction;
    public SandBoxDatabaseSetupFixture()
    {

        var dbContextBuilder = new DbContextOptionsBuilder<ProductDbContext>().UseSqlServer(_connectionString);
        Context = new ProductDbContext(dbContextBuilder.Options);

        _transaction = Context.Database.BeginTransaction();
    }
    public void Dispose()
    {
        _transaction.Rollback();
        _transaction.Dispose();
        Context.Dispose();
    }
}
