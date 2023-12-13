namespace Order.API.Test.Controllers.Test
{
    using Moq;
    using Order.API.Controllers;
    using Order.Domain.Common;
    using Order.Domain.Common.Resources;
    using Order.Domain.DTOs;
    using Order.Domain.Entities;
    using Order.Domain.Services;
    using System.Resources;

    public class ControllerTest
    {
        private readonly Mock<IProductService> _productService;
        public readonly ProductDTO _request;
        public readonly ProductController _productController;
        public readonly string productSuccessMsg;
        public readonly string productFailureMsg;
        public ControllerTest() 
        {
            _productService = new Mock<IProductService>();
            _productController = new ProductController(_productService.Object);
            _request = new ProductDTO() { Name = "TV", Description = "OLED TV", Price = 350000 };
            ResourceManager resourceManager = new ResourceManager(ResourceKeys.SystemMsgResrceName, typeof(MessageResources).Assembly);
            productSuccessMsg = resourceManager?.GetString(ResourceKeys.ProductSuccess) ?? string.Empty;           
            productFailureMsg = resourceManager?.GetString(ResourceKeys.ProductFailure) ?? string.Empty;
        }

        [Fact]
        public void Should_Controller_ReturnSuccess_OnPost()
        {
            //Arrange
             ApiResponse<bool> apiResponse = ApiResponse<bool>.Success(true,productSuccessMsg);
            _productService.Setup(a => a.CreateProduct(It.IsAny<ProductDTO>())).Returns(Task.FromResult(apiResponse));
             

            //Act
            var response = _productController.Post(_request);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Result.IsSuccess);
            Assert.Equal(productSuccessMsg, response.Result.Message);
        }

        [Fact]
        public void Should_Controller_ReturnFailure_OnPost()
        {
            //Arrange
            ApiResponse<bool> apiResponse = ApiResponse<bool>.Fail(productFailureMsg);
            _productService.Setup(a => a.CreateProduct(It.IsAny<ProductDTO>())).Returns(Task.FromResult(apiResponse));


            //Act
            var response = _productController.Post(_request);

            //Assert
            Assert.NotNull(response);
            Assert.True(!response.Result.IsSuccess);
            Assert.Equal(productFailureMsg, response.Result.Message);
        }
    }
}
