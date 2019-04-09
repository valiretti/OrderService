using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderService.DataProvider.Repositories;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Validators
{
    public class WorkTypeValidator : AbstractValidator<WorkTypeViewModel>
    {
        public WorkTypeValidator(IRepository<WorkType> repository)
        {
            RuleFor(w => w.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().MinimumLength(5)
                .Must(work => !repository.GetAll().Any(w => w.Name == work));
        }
    }
}
