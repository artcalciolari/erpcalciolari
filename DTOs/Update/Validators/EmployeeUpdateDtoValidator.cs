using FluentValidation;

namespace ErpCalciolari.DTOs.Update.Validators
{
    public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters")
                .When(x => x.Name != null);

            RuleFor(x => x.Username)
                .MaximumLength(50).WithMessage("Username can't be longer than 50 characters")
                .When(x => x.Username != null);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address")
                .When(x => x.Email != null);

            RuleFor(x => x.Password)
                .Length(4, 50).WithMessage("Password must be between 6 and 100 characters")
                .When(x => x.Password != null);
        }
    }
}
