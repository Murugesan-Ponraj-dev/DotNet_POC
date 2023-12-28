using System.Net;

namespace Order.Domain.Common;

/// <summary>
/// Common Api response model.
/// </summary>
/// <typeparam name="T">Result Type.</typeparam>
public class ApiResponse<T> where T : new()
{
    public HttpStatusCode StatusCode { get; set; }

    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public dynamic? Result { get; set; }

    public static ApiResponse<T> Fail(string? message)
    {
        return new ApiResponse<T> { Message = message, IsSuccess = false };
    }

    public static ApiResponse<T> Fail(Exception e)
    {
        return new ApiResponse<T> { Message = e?.Message, IsSuccess = false };
    }

    public static ApiResponse<T> Success(T value, string? message)
    {
        return new ApiResponse<T> { Result = value, Message = message, IsSuccess = true };
    }

    /// <summary>
    /// Set Success message .
    /// </summary>
    /// <param name="value">Result value.</param>
    /// <param name="message">response value.</param>
    /// <returns>Api response object.</returns>
    public static ApiResponse<T> Success(IEnumerable<T> value, string message)
    {
        return new ApiResponse<T> { Result = value, Message = message };
    }

}

