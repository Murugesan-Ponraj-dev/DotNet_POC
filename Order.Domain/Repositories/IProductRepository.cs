using Order.Domain.Entities;

namespace Order.Domain.Repositories;
public interface IProductRepository
{
    bool CreateProduct(Product product);
}

