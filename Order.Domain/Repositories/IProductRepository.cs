namespace Order.Domain.Repositories
{
    using Order.Domain.Entities;

    public interface IProductRepository
    {
        bool CreateProduct(Product product);
    }
}
