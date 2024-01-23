using Microsoft.AspNetCore.Mvc;
using Order.API.Common;
using Order.Domain.DTOs;
using Order.Domain.EntityResponse;
using Order.Domain.Services;

namespace Order.API.Controllers;

[Route(ApiControllerRouteConstant.ProductControllerURL)]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="productService">Product Service Object.</param>
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Create Product.
    /// </summary>
    /// <param name="productDTO"> Request data </param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [Route(ActionRouteConstant.CreateProductRoute)]
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]

    public async Task<ProductResponse> Post(ProductDTO productDTO)
    {
        return await _productService.CreateProduct(productDTO).ConfigureAwait(false);
    }

    [Route(ActionRouteConstant.GetAllProducts)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<ProductDTO>> GetAllProducts ()
    {
        return await _productService.GetAllProducts();
    }

    /// <summary>
    /// get filtered data
    /// </summary>
    /// <param name="filterQueryRequest"></param>
    /// <returns></returns>
    [Route(ActionRouteConstant.GetAllFilteredProducts)]
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<ProductDTO>> GetAllFilteredProducts(FilterQueryRequest filterQueryRequest)
    {
        return await _productService.GetFilteredData(filterQueryRequest).ConfigureAwait(false);
    }
}

