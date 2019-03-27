using FluentValidation;
using OrderService.DataProvider.Entities;

namespace OrderService.Logic.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.Description).NotEmpty();
            RuleFor(o => o.Name).NotEmpty();
            RuleFor(o => o.CustomerPhoneNumber).NotEmpty();
            RuleFor(o => o.CustomerUserId).NotEmpty();
        }
    }
}
