using FluentValidation;

namespace ErpCalciolari.DTOs.Create.Validators
{
    public class OrderItemCreateDtoValidator : AbstractValidator<OrderItemCreateDto>
    {
        public OrderItemCreateDtoValidator() 
        {
            RuleFor(o => o.ProductCode)
                .NotEmpty().WithMessage("ProductCode is required")
                .GreaterThan(0).WithMessage("ProductCode must be greater than 0");
            RuleFor(o => o.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
