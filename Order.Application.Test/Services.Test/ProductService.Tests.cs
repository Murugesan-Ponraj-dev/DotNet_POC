using AutoMapper;
using Moq;
using Order.Application.Services;
using Order.Domain.Common;
using Order.Domain.Common.Resources;
using Order.Domain.DTOs;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Domain.Services;
using System.Resources;

namespace Order.Application.Test.Services.Test;

/// <summary>
/// Test cases for Product services.
/// </summary>
public class ProductServiceTest
{
    private readonly ProductDTO _request;
    private readonly ProductService _service;
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<IMapper> _iMapper;
    private readonly Mock<IResourceManager> _ResourceManager;
    private readonly string productSuccessMsg;
    private readonly string productFailureMsg;
    private readonly string nullReferenceErrorMgs;
 

    public ProductServiceTest()
    {
        _iMapper = new Mock<IMapper>();
        _ResourceManager = new Mock<IResourceManager>();
        _productRepository = new Mock<IProductRepository>(); 
        _service = new ProductService(_productRepository.Object, _iMapper.Object, _ResourceManager.Object);       
        _request = new ProductDTO() { Name = RandomDataGenerator.GetRandomText(), Description = RandomDataGenerator.GetRandomText(), Price = RandomDataGenerator.GetRandomIntValue() };
        NullReferenceException exception = new NullReferenceException();
        nullReferenceErrorMgs = exception.Message;  
        
        // get and set resource manager application messages
        var resourceManager = new ResourceManager(ResourceKeys.SystemMsgResrceName, typeof(MessageResources).Assembly);
        productSuccessMsg = resourceManager?.GetString(ResourceKeys.ProductSuccess) ?? string.Empty;       
        productFailureMsg = resourceManager?.GetString(ResourceKeys.ProductFailure) ?? string.Empty;
    }

    [Fact]
    public void Should_ReturnSuccessRespone_OnDBSave()
    {
        //Arrange           
        _productRepository.Setup(a => a.CreateProduct(It.IsAny<Product>())).Returns(true);
        _iMapper.Setup(a => a.Map<ProductDTO, Product>(It.IsAny<ProductDTO>())).Returns(It.IsAny<Product>());
        _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(It.IsAny<string>(), It.IsAny<string>())).Returns(productSuccessMsg);

        //Act
         var response = _service.CreateProduct(_request);

        //Assert
        if (response is null)
            Assert.Fail(nullReferenceErrorMgs);
        Assert.True(response.Result.IsSuccess);
        Assert.Equal(productSuccessMsg, response.Result.Message);
    }

    [Fact]
    public void Should_ReturnFailureResponse_OnDBFaileToSave()
    {
        //Arrange            
        _productRepository.Setup(a => a.CreateProduct(It.IsAny<Product>())).Returns(false);
        _iMapper.Setup(a => a.Map<ProductDTO, Product>(It.IsAny<ProductDTO>())).Returns(It.IsAny<Product>());
        _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(It.IsAny<string>(), It.IsAny<string>())).Returns(productFailureMsg);

        //Act
        var response = _service.CreateProduct(_request);


        //Assert
        if (response is null)
            Assert.Fail(nullReferenceErrorMgs);
        Assert.True(!response.Result.IsSuccess);
        Assert.Equal(productFailureMsg, response.Result.Message);
    }

    [Fact]
    public void Should_Through_Exception_OnNullMapperObject()
    {
        //Arrange    
       // Remove Setup file for product repo
        _productRepository.Setup(a => a.CreateProduct(It.IsAny<Product>())).Returns(false);
        _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(It.IsAny<string>(), It.IsAny<string>())).Returns(productFailureMsg);
        // set mapper object as null
        IMapper? resourceObject = null;
        var service = new ProductService(_productRepository.Object, resourceObject, _ResourceManager.Object);

        //Act
        var response = service.CreateProduct(_request);

        //Assert
        if (response is null)
            Assert.Fail(nullReferenceErrorMgs);
        Assert.True(!response.Result.IsSuccess);
        Assert.Equal(nullReferenceErrorMgs, response.Result.Message);
    }
}
