

using Application.Features.Products.Commands;
using FluentValidation;

namespace Application.DTo.Products.Validations;

public class AddProductValidation : AbstractValidator<AddProductRequest>
{
    public AddProductValidation()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice is required");
    }
}
