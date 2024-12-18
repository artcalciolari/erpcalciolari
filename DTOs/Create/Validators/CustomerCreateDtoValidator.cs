using FluentValidation;

namespace ErpCalciolari.DTOs.Create.Validators
{
    public class CustomerCreateDtoValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .MaximumLength(20).WithMessage("Phone can't be longer than 20 characters");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address")
                .When(x => x.Email != null);
            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Address can't be longer than 200 characters")
                .When(x => x.Address != null);
        }
    }
}
