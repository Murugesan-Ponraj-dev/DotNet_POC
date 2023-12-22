using Microsoft.AspNetCore.Mvc;
using Order.API.Common;
using Order.Domain.DTOs;
using Order.Domain.EntityResponse;
using Order.Domain.Services;

namespace Order.API.Controllers;

[Route(WebApiRouteContent.ProductControllerURL)]
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

   
    public async Task<ProductResponse> Post(ProductDTO productDTO)
    {
        return await _productService.CreateProduct(productDTO).ConfigureAwait(false);
    }
}

