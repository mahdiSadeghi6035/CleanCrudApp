

using Application.Features.Products.Commands;
using FluentValidation;

namespace Application.DTo.Products.Validations;

public class UpdateProductValidation : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidation()
    {
        RuleFor(p => p.Id).GreaterThan(0).WithMessage("Id is required");
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice is required");
    }
}
