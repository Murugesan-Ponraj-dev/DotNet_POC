namespace Order.Domain.Services
{
    using Order.Domain.Common;
    using Order.Domain.DTOs;

    public interface IProductService
    {
        Task<ApiResponse<bool>> CreateProduct(ProductDTO productDTO);

    }
}
