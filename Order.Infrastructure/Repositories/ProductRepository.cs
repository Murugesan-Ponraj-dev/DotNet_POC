using MongoDB.Driver;
using Order.Domain.Common;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using System.Data;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;
        public ProductRepository(IMongoDatabase mongoDatabase)
        {
            _productCollection = mongoDatabase.GetCollection<Product>(AppConfigConstant.ProductCollection);
        }
        public Product CreateProduct(Product product)
        {
            if (product.Id is not null)
            {
                _productCollection.InsertOne(product);
            }
            return product;           
        }

        public Product GetProduct(string id)
        {
            return _productCollection.Find(m => m.Id == id).FirstOrDefault();
        }

        public bool DeleteProduct(string id)
        {
            var product = GetProduct(id);
            if (product is not null)
            {
                _productCollection.DeleteOne(m => m.Id == id);
                return true;
            }
            return false;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _productCollection.Find(a => true).ToList();
        }

        public Product GetProduct(Expression<Func<Product, bool>> filterCondition)
        {
            return _productCollection.Find(filterCondition).FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> filterCondition)
        {
            return _productCollection.Find(filterCondition).ToList();
        }

        public Product UpdateProduct(Product product)
        {
            _productCollection.ReplaceOne(m=>m.Id == product.Id,product);
            return product;
        }
    }
}
