using Order.Domain.DTOs;
using Order.Domain.EntityResponse;

namespace Order.Domain.Services;
public interface IProductService
{
    Task<ProductResponse> CreateProduct(ProductDTO productDTO);

}

