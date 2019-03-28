using FluentValidation;
using OrderService.Model;

namespace OrderService.Logic.Validators
{
    public class RegistrationViewModelValidator: AbstractValidator<RegisterViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Password length must be greater than 3");
        }
    }
}
