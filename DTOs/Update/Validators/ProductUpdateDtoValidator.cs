using FluentValidation;

namespace ErpCalciolari.DTOs.Update.Validators
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Code)
                .GreaterThan(0).WithMessage("Code must be greater than 0")
                .When(x => x.Code != null);

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters")
                .When(x => x.Name != null);

            RuleFor(x => x.Type)
                .MaximumLength(50).WithMessage("Type can't be longer than 50 characters")
                .When(x => x.Type != null);

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0")
                .When(x => x.Quantity != null);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .When(x => x.Price != null);
        }
    }
}
