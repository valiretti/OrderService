using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderService.DataProvider.Entities;
using OrderService.DataProvider.Repositories;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly ICommitProvider _commitProvider;
        private readonly IValidator<Order> _validator;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> repository, ICommitProvider commitProvider, IValidator<Order> validator, IMapper mapper)
        {
            _repository = repository;
            _commitProvider = commitProvider;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Order> Create(Order order)
        {
            var result = _validator.Validate(order);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            order.CreationDate = DateTime.UtcNow;
            await _repository.Create(order);
            await _commitProvider.SaveAsync();

            return order;
        }

        public async Task Update(Order item)
        {
            var order = _repository.GetAll().SingleOrDefault(o => o.Id == item.Id);
            if (order == null)
            {
                throw new ValidationException("The order doesn't exist");
            }

            var result = _validator.Validate(order);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            _repository.Update(item);

            await _commitProvider.SaveAsync();
        }

        public async Task<OrderPage> GetPage(int pageNumber, int pageSize)
        {
           var orders = await _repository.GetAll()
                .OrderBy(x => x.CreationDate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
           var totalCount = await _repository.GetAll().CountAsync();

           return new OrderPage
           {
               Orders = _mapper.Map<IEnumerable<OrderViewModel>>(orders),
               TotalCount = totalCount
           };
        }

        public async Task Delete(int id)
        {
            var order = _repository.GetAll().SingleOrDefault(o => o.Id == id);
            if (order != null)
            {
                await _repository.Delete(id);
                await _commitProvider.SaveAsync();
            }
        }
    }
}
