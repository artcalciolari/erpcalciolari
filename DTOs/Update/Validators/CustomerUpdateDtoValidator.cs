using FluentValidation;

namespace ErpCalciolari.DTOs.Update.Validators
{
    public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateDtoValidator() 
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters")
                .When(x => x.Name != null);
            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone can't be longer than 20 characters")
                .When(x => x.Phone != null);
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address")
                .When(x => x.Email != null);
            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Address can't be longer than 200 characters")
                .When(x => x.Address != null);
        }
    }
}
