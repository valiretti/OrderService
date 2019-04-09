using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OrderService.DataProvider.Repositories;
using OrderService.Model;
using OrderService.Model.Entities;

namespace OrderService.Logic.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<CustomerRequest> _customerRepository;
        private readonly IRepository<ExecutorRequest> _executorRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RequestService(
            IRepository<CustomerRequest> customerRepository,
            IRepository<ExecutorRequest> executorRepository,
            IUserService userService,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _executorRepository = executorRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<RequestViewModel> CreateExecutorRequest(CreateRequestModel request)
        {
            var executorId = await _userService.GetExecutorIdByUserId(request.UserId);
            request.ExecutorId = executorId;
            var executorRequest = _mapper.Map<ExecutorRequest>(request);
            executorRequest.RequestStatus = RequestStatus.New;
            await _executorRepository.Create(executorRequest);

            return _mapper.Map<RequestViewModel>(executorRequest);
        }

        public async Task<RequestViewModel> CreateCustomerRequest(CreateRequestModel request)
        {
            var customerRequest = _mapper.Map<CustomerRequest>(request);
            customerRequest.RequestStatus = RequestStatus.New;
            await _customerRepository.Create(customerRequest);

            return _mapper.Map<RequestViewModel>(customerRequest);
        }

        public Task Update(UpdateRequestModel request)
        {
            throw new NotImplementedException();
        }

        public Task<RequestPage> GetExecutorRequests(int pageNumber, int pageSize, int executorId)
        {
            throw new NotImplementedException();
        }

        public Task<RequestPage> GetCustomerRequests(int pageNumber, int pageSize, string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
