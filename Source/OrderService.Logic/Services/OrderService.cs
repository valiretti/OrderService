﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderService.DataProvider.Repositories;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IPhotoService _photoService;
        private readonly ICommitProvider _commitProvider;
        private readonly IValidator<CreateOrderModel> _validator;
        private readonly IMapper _mapper;

        public OrderService(
            IRepository<Order> orderRepository,
            IPhotoService photoService,
            ICommitProvider commitProvider,
            IValidator<CreateOrderModel> validator,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _photoService = photoService;
            _commitProvider = commitProvider;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> Create(CreateOrderModel item)
        {
            var result = _validator.Validate(item);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var order = _mapper.Map<Order>(item);
            order.CreationDate = DateTime.UtcNow;
            order.Status = OrderStatus.Active;

            await AddPhotos(item, order);
            await _orderRepository.Create(order);
            await _commitProvider.SaveAsync();

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task Update(UpdateOrderModel item)
        {
            var result = _validator.Validate(item);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var order = _orderRepository.GetAll()
                .Include(o => o.Photos)
                .SingleOrDefault(o => o.Id == item.Id);
            if (order == null)
            {
                throw new ValidationException("The order doesn't exist");
            }

            _mapper.Map(item, order);
            if (item.ExistingPhotos != null)
            {
                await _photoService.DeletePhotosByPathsByOrderId(item.ExistingPhotos, item.Id);
            }

            await AddPhotos(item, order);
            _orderRepository.Update(order);
            await _commitProvider.SaveAsync();
        }

        public async Task<OrderPage> GetPage(int pageNumber, int pageSize)
        {
            var orders = await _orderRepository.GetAll()
                .Include(o => o.Photos)
                .Include(o => o.WorkType)
                .Where(o => o.Status == OrderStatus.Active && o.ExecutorId == null)
                .OrderByDescending(x => x.CreationDate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalCount = await _orderRepository.GetAll()
                .Where(o => o.Status == OrderStatus.Active && o.ExecutorId == null)
                .CountAsync();

            return new OrderPage
            {
                Orders = _mapper.Map<IEnumerable<OrderPageViewModel>>(orders),
                TotalCount = totalCount
            };
        }

        public async Task<OrderViewModel> Get(int id)
        {
            var order = await _orderRepository.GetAll()
                .Include(o => o.WorkType)
                .Include(o => o.Photos)
                .Include(o => o.Executor)
                .Include(o => o.Customer)
                .SingleOrDefaultAsync(o => o.Id == id);

            return order == null ? null : _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderPage> GetPageByCustomerId(int pageNumber, int pageSize, string customerId)
        {
            var orders = await _orderRepository.GetAll()
                .Include(o => o.Photos)
                .Include(o => o.WorkType)
                .Where(x => x.CustomerUserId == customerId)
                .OrderByDescending(x => x.CreationDate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalCount = await _orderRepository.GetAll()
                .Where(x => x.CustomerUserId == customerId)
                .CountAsync();

            return new OrderPage
            {
                Orders = _mapper.Map<IEnumerable<OrderPageViewModel>>(orders),
                TotalCount = totalCount
            };
        }

        public async Task Delete(int id)
        {
            var order = _orderRepository.GetAll().SingleOrDefault(o => o.Id == id);
            if (order == null)
            {
                throw new ValidationException("The order doesn't exist");
            }

            await _orderRepository.Delete(id);
            await _commitProvider.SaveAsync();
        }

        private async Task AddPhotos(CreateOrderModel item, Order order)
        {
            if (item.Photos != null)
            {
                var photos = await _photoService.GetByIds(item.Photos);
                order.Photos = order.Photos ?? new List<Photo>();
                foreach (var photo in photos)
                {
                    order.Photos.Add(photo);
                }
            }
        }
    }
}
