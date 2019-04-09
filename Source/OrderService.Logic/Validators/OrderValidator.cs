using FluentValidation;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Validators
{
    public class OrderValidator : AbstractValidator<CreateOrderModel>
    {
        public OrderValidator()
        {
            RuleFor(o => o.Description).NotEmpty();
            RuleFor(o => o.Location).NotEmpty();
            RuleFor(o => o.Name).NotEmpty();
            RuleFor(o => o.CustomerPhoneNumber).NotEmpty();
            RuleFor(o => o.CustomerUserId).NotEmpty();
        }
    }
}
