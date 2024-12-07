using FluentValidation;

namespace ErpCalciolari.DTOs.Update
{
    public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");

            RuleFor(x => x.Username)
                .MaximumLength(50).WithMessage("Username can't be longer than 50 characters");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Password)
                .Length(6, 100).WithMessage("Password must be between 6 and 100 characters");
        }
    }
}
