using AutoMapper;
using Order.Domain.Common;
using Order.Domain.Common.Resources;
using Order.Domain.DTOs;
using Order.Domain.Entities;
using Order.Domain.EntityResponse;
using Order.Domain.Repositories;
using Order.Domain.Services;
using System.Collections.Generic;

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
            var productEntity = _mapper.Map<Product>(productDTO);
            var product = _productRepository.CreateProduct(productEntity);
            if (product != null && string.IsNullOrEmpty(product.Id))
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

    public Task<IEnumerable<ProductDTO>> GetAllProducts()
    {
        IEnumerable<ProductDTO> productList = new List<ProductDTO>();
        try 
        {
            var products= _productRepository.GetAllProduct();
            if (products is null && products.Count() == 0)
                return Task.FromResult(productList);
            var productEntity = _mapper.Map<IEnumerable<Product>>(products);
            return Task.FromResult(productList);
        }
        catch (Exception)
        {           
            return Task.FromResult(productList);
        }
    }
}

