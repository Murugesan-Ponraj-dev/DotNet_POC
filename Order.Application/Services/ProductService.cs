using AutoMapper;
using Order.Domain.Common;
using Order.Domain.Common.Resources;
using Order.Domain.DTOs;
using Order.Domain.Entities;
using Order.Domain.EntityResponse;
using Order.Domain.Repositories;
using Order.Domain.Services;

namespace Order.Application.Services;

/// <summary>
/// Product service.
/// </summary>
public class ProductService : IProductService
{

    public readonly IProductRepository _productRepository;
    public readonly IMapper _mapper;
    public readonly IResourceManager _resourceManager;

    public ProductService(IProductRepository productRepository, IMapper mapper, IResourceManager resourceManager)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _resourceManager = resourceManager;
    }

    /// <summary>
    /// Create Product.
    /// </summary>
    /// <param name="productDTO">Request data</param>
    /// <returns>Response Data.</returns>
    public Task<ProductResponse> CreateProduct(ProductDTO productDTO)
    {
        ProductResponse productResponse = new ProductResponse();
        try
        {
            if (productDTO is null)
            {
                throw new ArgumentNullException(nameof(productDTO));
            }
            string successMesage = _resourceManager.GetResourceValue<MessageResources>(ResourceKeys.SystemMsgResrceName, ResourceKeys.ProductSuccess);
            string failureMessage = _resourceManager.GetResourceValue<MessageResources>(ResourceKeys.SystemMsgResrceName, ResourceKeys.ProductFailure);
            var product = _mapper.Map<Product>(productDTO);
            bool isSuccess = _productRepository.CreateProduct(product);
            if (!isSuccess)
                productResponse.Fail(failureMessage);
            else
                productResponse.Success(productDTO, successMesage);
            return Task.FromResult(productResponse);
        }
        catch (Exception ex)
        {
            productResponse.Fail(ex.Message);
            return Task.FromResult(productResponse);
        }
    }
}

