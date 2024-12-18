using FluentValidation;
using ErpCalciolari.DTOs.Create.Validators;

namespace ErpCalciolari.DTOs.Update.Validators
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(o => o.CustomerName)
                .MaximumLength(100).WithMessage("Name can't be greater than 100 characters")
                .When(o => o.CustomerName != null);
            RuleFor(o => o.DeliveryDate)
                .GreaterThan(System.DateTime.Now).WithMessage("Delivery date must be greater than today")
                .When(o => o.DeliveryDate != null);
            RuleForEach(o => o.Items)
                .SetValidator(new OrderItemCreateDtoValidator())
                .When(o => o.Items != null);
        }
    }
}
