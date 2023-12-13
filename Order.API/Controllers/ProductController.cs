namespace Order.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Order.API.Common;
    using Order.Domain.Common;
    using Order.Domain.DTOs;
    using Order.Domain.Services;

    [Route(WebApiRouteContant.CreateProductURL)]
    [ApiController]
    public class ProductController : ControllerBase
    {
       private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">Product Service Objet.</param>
       public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Create Products.
        /// </summary>
        /// <param name="productDTO"> Request data </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        public async Task<ApiResponse<bool>> Post(ProductDTO productDTO)
        {
            return await this._productService.CreateProduct(productDTO);
        }
    }
}
