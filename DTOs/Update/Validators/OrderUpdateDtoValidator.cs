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
            RuleFor(o => o.Status)
                .MaximumLength(20).WithMessage("Status can't be greater than 20 characters")
                .Must(s => s == "PENDENTE" || s == "OK").WithMessage("Status must be Pending or OK")
                .When(o => o.Status != null);
            RuleForEach(o => o.Items)
                .SetValidator(new OrderItemCreateDtoValidator())
                .When(o => o.Items != null);
        }
    }
}
