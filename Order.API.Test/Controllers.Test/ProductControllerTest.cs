using Moq;
using Order.API.Controllers;
using Order.Domain.Common;
using Order.Domain.Common.Resources;
using Order.Domain.DTOs;
using Order.Domain.EntityResponse;
using Order.Domain.Services;
using System.Resources;

namespace Order.API.Test.Controllers.Test;

public class ProductControllerTest
{
    private readonly Mock<IProductService> _productService;
    private readonly ProductDTO _request;
    private readonly ProductController _productController;
    private readonly string productSuccessMsg;
    private readonly string productFailureMsg;
    public ProductControllerTest()
    {
        _productService = new Mock<IProductService>();
        _productController = new ProductController(_productService.Object);        
        _request = new ProductDTO() { Name = RandomDataGenerator.GetRandomText(), Description = RandomDataGenerator.GetRandomText(), Price = RandomDataGenerator.GetRandomIntValue() };
        ResourceManager resourceManager = new(ResourceKeys.SystemMsgResrceName, typeof(MessageResources).Assembly);
        productSuccessMsg = resourceManager?.GetString(ResourceKeys.ProductSuccess) ?? string.Empty;
        productFailureMsg = resourceManager?.GetString(ResourceKeys.ProductFailure) ?? string.Empty;
    }

    [Fact]
    public void Should_Controller_ReturnSuccess_ProductService_Save()
    {
        //Arrange
        var mockApiResponse = new ProductResponse() { IsSuccess = true, Message = productSuccessMsg, Result = _request };
        _productService.Setup(a => a.CreateProduct(It.IsAny<ProductDTO>())).ReturnsAsync(mockApiResponse);


        //Act
        Task<ProductResponse> response = _productController.Post(_request);

        //Assert
        Assert.NotNull(response);
        Assert.True(response.Result.IsSuccess);
        Assert.Equal(productSuccessMsg, response.Result.Message);
    }

    [Fact]
    public void Should_Controller_ReturnFailure_On_ProductService_FailToSave()
    {
        //Arrange
         var mockapiResponse = new ProductResponse() { IsSuccess = false, Message = productFailureMsg, Result = null };
        _productService.Setup(a => a.CreateProduct(_request)).ReturnsAsync(mockapiResponse);


        //Act
        Task<ProductResponse> response = _productController.Post(_request);

        //Assert
        Assert.NotNull(response);
        Assert.True(!response.Result.IsSuccess);
        Assert.Equal(productFailureMsg, response.Result.Message);
    }


    [Fact]
    public void Should_Controller_ThrowExceptionOn_ServiceIsNull()
    {
        //Arrange
        var mockapiResponse = new ProductResponse() { IsSuccess = false, Message = productFailureMsg, Result = null };
        _productService.Setup(a => a.CreateProduct(_request)).ReturnsAsync(mockapiResponse);


        //Act
        Task<ProductResponse> response = _productController.Post(_request);

        //Assert
        Assert.NotNull(response);
        Assert.True(!response.Result.IsSuccess);
        Assert.Equal(productFailureMsg, response.Result.Message);
    }
}

