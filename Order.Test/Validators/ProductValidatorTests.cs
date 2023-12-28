using FluentValidation.TestHelper;
using Order.Domain.DTOs;
using Order.Infrastructure.Validators;
using Xunit;

namespace Order.Test.Validators;
public class ProductValidatorTests
{
    private readonly ProductDtoValidator validator;

    public ProductValidatorTests()
    {
        validator = new ProductDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_Name_IsNull()
    {
        //Arrange
        var model = new ProductDTO() { Name = null };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(a => a.Name);
    }

    [Fact]
    public void Should_Have_Error_Name_IsEmpty()
    {
        //Arrange
        var model = new ProductDTO() { Name = string.Empty };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(a => a.Name);
    }

    [Fact]
    public void Should_Have_Error_Desciption_IsNull()
    {
        //Arrange
        var model = new ProductDTO() { Description = null };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(a => a.Description);
    }

    [Fact]
    public void Should_Have_Error_Description_IsEmpty()
    {
        //Arrange
        var model = new ProductDTO() { Description = string.Empty };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(a => a.Description);
    }
    [Fact]
    public void Should_Have_Error_Price_IsNull()
    {
        //Arrange
        var model = new ProductDTO() { Price = null };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(a => a.Price);
    }
}
