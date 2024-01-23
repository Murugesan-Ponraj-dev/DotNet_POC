using MongoDB.Driver;
using Order.Domain.Common;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories;

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
        _productCollection.ReplaceOne(m => m.Id == product.Id, product);
        return product;
    } 

    public (int totalPages, IEnumerable<Product> data) GetAllDataByBrandThroughLinq(string brand, int page = 1, int pageSize = 10)
    {
        var sortDefinition = Builders<Product>.Sort.Ascending(x => x.Price);
        var data =   _productCollection.Find(a=>a.Brand == brand)
            .Sort(sortDefinition)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToList();
        var count =   Convert.ToInt32(_productCollection.CountDocumentsAsync(a => a.Brand == brand).Result);
        return (count, data); 
    }

    public (int totalPages, IReadOnlyList<Product> data) GetFilteredData(FilterDefinition<Product> filterDefinition, int page = 1, int pageSize = 10)
    {
        var response = FilterData(filterDefinition, page, pageSize).Result;
        return response;
    }

    public async Task<(int totalPages, IReadOnlyList<Product> data)> FilterData(FilterDefinition<Product> filterDefinition,int page = 1, int pageSize= 10)
    {
        var collection = _productCollection;
        var countFacet = AggregateFacet.Create("count",
            PipelineDefinition<Product, AggregateCountResult>.Create(new[]
            {
            PipelineStageDefinitionBuilder.Count<Product>()
            }));
        var sortDefinition = Builders<Product>.Sort.Ascending(x => x.Price);
        var dataFacet = AggregateFacet.Create("data",
            PipelineDefinition<Product, Product>.Create(new[]
            {               
            PipelineStageDefinitionBuilder.Sort(sortDefinition),
            PipelineStageDefinitionBuilder.Skip<Product>((page - 1) * pageSize),
            PipelineStageDefinitionBuilder.Limit<Product>(pageSize),
            })); ;


        var aggregation = await collection.Aggregate()
            .Match(filterDefinition)
            .Facet(countFacet, dataFacet)
            .ToListAsync();

        var count = aggregation.First()
            .Facets.First(x => x.Name == "count")
            .Output<AggregateCountResult>()
            ?.FirstOrDefault()
            ?.Count;

        var totalPages = (int)Math.Ceiling((double)count / pageSize);

        var data = aggregation.First()
            .Facets.First(x => x.Name == "data")
            .Output<Product>();

        return (totalPages, data);
    } 
 
}
