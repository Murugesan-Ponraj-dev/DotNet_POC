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
    private readonly Product _product;
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
        _product = new Product() { Name = RandomDataGenerator.GetRandomText(), Description = RandomDataGenerator.GetRandomText(), Price = RandomDataGenerator.GetRandomIntValue() };
        NullReferenceException exception = new NullReferenceException();
        nullReferenceErrorMgs = exception.Message;  
        
        // get and set resource manager application messages
        var resourceManager = new ResourceManager(ResourceKeys.SystemMsgResrceName, typeof(MessageResources).Assembly);
        productSuccessMsg = resourceManager?.GetString(ResourceKeys.ProductSuccess) ?? string.Empty;       
        productFailureMsg = resourceManager?.GetString(ResourceKeys.ProductFailure) ?? string.Empty;
        //Common setup
        _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(ResourceKeys.SystemMsgResrceName, ResourceKeys.ProductSuccess)).Returns(productSuccessMsg);
        _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(ResourceKeys.SystemMsgResrceName, ResourceKeys.ProductFailure)).Returns(productFailureMsg);
        _iMapper.Setup(a => a.Map<Product>(_request)).Returns(_product);

    }

    [Fact]
    public void Should_ReturnSuccessResponse_OnDBSave()
    {
        //Arrange           
        _productRepository.Setup(a => a.CreateProduct(_product)).Returns(true);
        
      
        //Act
        var response = _service.CreateProduct(_request);

        //Assert
        if (response is null)
            Assert.Fail(nullReferenceErrorMgs);
        Assert.True(response.Result.IsSuccess);
        Assert.Equal(productSuccessMsg, response.Result.Message);
    }

    [Fact]
    public void Should_ReturnFailureResponse_OnDBFailToSave()
    {
        //Arrange            
        _productRepository.Setup(a => a.CreateProduct(_product)).Returns(false);
    
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
        _productRepository.Setup(a => a.CreateProduct(_product)).Returns(false);
      //  _ResourceManager.Setup(a => a.GetResourceValue<MessageResources>(It.IsAny<string>(), It.IsAny<string>())).Returns(productFailureMsg);
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
