using FluentValidation;
using OrderService.DataProvider.Entities;

namespace OrderService.Logic.Validators
{
    public class WorkTypeValidator : AbstractValidator<WorkType>
    {
        public WorkTypeValidator()
        {
            RuleFor(w => w.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().MinimumLength(5);
        }
    }
}
