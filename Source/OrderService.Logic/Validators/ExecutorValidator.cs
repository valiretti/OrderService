using FluentValidation;
using OrderService.DataProvider.Entities;

namespace OrderService.Logic.Validators
{
    public class ExecutorValidator : AbstractValidator<Executor>
    {
        public ExecutorValidator()
        {
            RuleFor(e => e.FullName).NotEmpty();
            RuleFor(e => e.OrganizationName).NotEmpty();
            RuleFor(e => e.UserId).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
            RuleFor(e => e.PhoneNumber).NotEmpty();
        }
    }
}
