using FluentValidation;
using Order.Domain.DTOs;

namespace Order.Infrastructure.Validators;
public class ProductDtoValidator : AbstractValidator<ProductDTO>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Description).NotNull().NotEmpty();
        RuleFor(x => x.Price).NotNull().NotEmpty();
    }
}
