using MongoDB.Driver;
using Order.Domain.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Order.Domain.Repositories;
public interface IProductRepository
{
    Product CreateProduct(Product product);

    Product UpdateProduct(Product product);

    IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> filterCondition);

    Product GetProduct(Expression<Func<Product, bool>> filterCondition);

    IEnumerable<Product> GetAllProduct();

    bool DeleteProduct(string id);

    public (int totalPages, IReadOnlyList<Product> data) GetFilteredData(FilterDefinition<Product> filterDefinition, int page = 1, int pageSize = 10);
}

