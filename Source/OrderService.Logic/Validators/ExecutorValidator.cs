using FluentValidation;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Validators
{
    public class ExecutorValidator : AbstractValidator<CreateExecutorModel>
    {
        public ExecutorValidator()
        {
            RuleFor(e => e.OrganizationName).NotEmpty();
            RuleFor(e => e.UserId).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
            RuleFor(e => e.PhoneNumber).NotEmpty();
        }
    }
}
