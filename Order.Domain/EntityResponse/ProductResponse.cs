using Order.Domain.DTOs;

namespace Order.Domain.EntityResponse;
public class ProductResponse
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public ProductDTO? Result { get; set; }

    public void Fail(string? message)
    {
        Message = message;
        IsSuccess = false;
    }
 
    public void Success(ProductDTO value, string? message)
    {
        Result = value;
        Message = message;
        IsSuccess = true;
    }
}

