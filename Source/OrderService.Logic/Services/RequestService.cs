using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderService.DataProvider.Repositories;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<CustomerRequest> _customerRequestRepository;
        private readonly IRepository<ExecutorRequest> _executorRequestRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IUserService _userService;
        private readonly ICommitProvider _commitProvider;
        private readonly IMapper _mapper;

        public RequestService(
            IRepository<CustomerRequest> customerRequestRepository,
            IRepository<ExecutorRequest> executorRequestRepository,
            IRepository<Order> orderRepository,
            IUserService userService,
            ICommitProvider commitProvider,
            IMapper mapper)
        {
            _customerRequestRepository = customerRequestRepository;
            _executorRequestRepository = executorRequestRepository;
            _orderRepository = orderRepository;
            _userService = userService;
            _commitProvider = commitProvider;
            _mapper = mapper;
        }

        public async Task<RequestViewModel> CreateExecutorRequest(CreateRequestModel request)
        {
            var executorId = await _userService.GetExecutorIdByUserId(request.UserId);
            request.ExecutorId = executorId;
            var executorRequest = _mapper.Map<ExecutorRequest>(request);
            executorRequest.RequestStatus = RequestStatus.New;
            await _executorRequestRepository.Create(executorRequest);
            await _commitProvider.SaveAsync();

            return _mapper.Map<RequestViewModel>(executorRequest);
        }

        public async Task<RequestViewModel> CreateCustomerRequest(CreateRequestModel request)
        {
            var customerRequest = _mapper.Map<CustomerRequest>(request);
            customerRequest.RequestStatus = RequestStatus.New;
            await _customerRequestRepository.Create(customerRequest);
            await _commitProvider.SaveAsync();

            return _mapper.Map<RequestViewModel>(customerRequest);
        }

        public async Task MarkExecutorRequestAccepted(int requestId, string customerId)
        {
            var request = await _executorRequestRepository.GetAll().FirstOrDefaultAsync(r => r.Id == requestId);
            if (request == null)
            {
                throw new ValidationException("The request doesn't exist");
            }

            var order = await _orderRepository.GetAll()
                .SingleOrDefaultAsync(o => o.CustomerUserId == customerId && o.Id == request.OrderId);
            if (order == null)
            {
                throw new ValidationException("The order doesn't exist");
            }

            request.RequestStatus = RequestStatus.Accepted;
            order.OrderStatus = OrderStatus.Confirmed;
            order.ExecutorId = request.ExecutorId;
            await _commitProvider.SaveAsync();
        }

        public async Task MarkCustomerRequestAccepted(int requestId)
        {
            var request = await _customerRequestRepository.GetAll().FirstOrDefaultAsync(r => r.Id == requestId);
            if (request == null)
            {
                throw new ValidationException("The request doesn't exist");
            }
            var order = await _orderRepository.GetAll()
                .SingleOrDefaultAsync(o => o.Id == request.OrderId);
            if (order == null)
            {
                throw new ValidationException("The order doesn't exist");
            }

            request.RequestStatus = RequestStatus.Accepted;
            order.OrderStatus = OrderStatus.Confirmed;
            order.ExecutorId = request.ExecutorId;
            await _commitProvider.SaveAsync();
        }

        public async Task<RequestViewModel> GetExecutorRequest(int id)
        {
            var request = await _executorRequestRepository.GetAll()
                .Include(r => r.Executor)
                .Include(r => r.Order)
                .SingleOrDefaultAsync(r => r.Id == id);
            return request == null ? null : _mapper.Map<RequestViewModel>(request);
        }

        public async Task<RequestViewModel> GetCustomerRequest(int id)
        {
            var request = await _customerRequestRepository.GetAll()
                .Include(r => r.Executor)
                .Include(r => r.Order)
                .SingleOrDefaultAsync(r => r.Id == id);
            return request == null ? null : _mapper.Map<RequestViewModel>(request);
        }

        public async Task<RequestPage> GetNewExecutorRequests(int pageNumber, int pageSize, string userId)
        {
            var requests = await _executorRequestRepository.GetAll()
                .Include(r => r.Executor)
                .Include(r => r.Order)
                .Where(r => r.Order.CustomerUserId == userId && r.RequestStatus == RequestStatus.New)
                .OrderByDescending(x => x.CreationDate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalCount = await _executorRequestRepository.GetAll()
                .Where(r => r.Order.CustomerUserId == userId && r.RequestStatus == RequestStatus.New)
                .CountAsync();

            return new RequestPage
            {
                Requests = _mapper.Map<IEnumerable<RequestViewModel>>(requests),
                TotalCount = totalCount
            };
        }

        public async Task<RequestPage> GetNewCustomerRequests(int pageNumber, int pageSize, string userId)
        {
            var executorId = await _userService.GetExecutorIdByUserId(userId);
            var requests = await _customerRequestRepository.GetAll()
                .Include(r => r.Executor)
                .Include(r => r.Order)
                .Where(r => r.ExecutorId == executorId && r.RequestStatus == RequestStatus.New)
                .OrderByDescending(x => x.CreationDate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalCount = await _customerRequestRepository.GetAll()
                .Where(r => r.ExecutorId == executorId && r.RequestStatus == RequestStatus.New)
                .CountAsync();

            return new RequestPage
            {
                Requests = _mapper.Map<IEnumerable<RequestViewModel>>(requests),
                TotalCount = totalCount
            };
        }
    }
}
