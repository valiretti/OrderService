using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderService.DataProvider.Entities;
using OrderService.DataProvider.Repositories;

namespace OrderService.Logic.Validators
{
    public class WorkTypeValidator : AbstractValidator<WorkType>
    {
        public WorkTypeValidator(IRepository<WorkType> repository)
        {
            RuleFor(w => w.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().MinimumLength(5)
                .Must(work => !repository.GetAll().Any(w => w.Name == work));
        }
    }
}
