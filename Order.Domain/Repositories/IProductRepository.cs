using Order.Domain.Entities;
using System.Linq.Expressions;

namespace Order.Domain.Repositories;
public interface IProductRepository
{
    Product CreateProduct(Product product);

    Product UpdateProduct(Product product);

    IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> filterCondition);

    Product GetProduct(Expression<Func<Product, bool>> filterCondition);

    IEnumerable<Product> GetAllProduct();

    bool DeleteProduct(string id);
}

