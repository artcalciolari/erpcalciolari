using FluentValidation;
using ErpCalciolari.DTOs.Create.Validators;

namespace ErpCalciolari.DTOs.Create.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator() 
        {
            RuleFor(o => o.CustomerName)
                .NotEmpty().WithMessage("CustomerName is required")
                .MaximumLength(100).WithMessage("Name can't be greater than 100 characters");
            RuleFor(o => o.OrderNumber)
                .NotEmpty().WithMessage("OrderNumber is required")
                .GreaterThan(0).WithMessage("Code must be greater than 0");
            RuleFor(o => o.DeliveryDate)
                .NotEmpty().WithMessage("DeliveryDate is required")
                .GreaterThan(DateTime.Now).WithMessage("DeliveryDate must be greater than today");
            RuleForEach(o => o.Items)
                .SetValidator(new OrderItemCreateDtoValidator());
        }
    }
}
