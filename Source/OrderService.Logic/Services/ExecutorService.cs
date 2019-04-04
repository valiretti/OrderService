using System;
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
    public class ExecutorService : IExecutorService
    {
        private readonly IRepository<Executor> _repository;
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;
        private readonly ICommitProvider _commitProvider;
        private readonly IValidator<Executor> _validator;
        private readonly IMapper _mapper;

        public ExecutorService(IRepository<Executor> repository, IPhotoService photoService, IUserService userService, ICommitProvider commitProvider, IValidator<Executor> validator, IMapper mapper)
        {
            _repository = repository;
            _photoService = photoService;
            _userService = userService;
            _commitProvider = commitProvider;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ExecutorViewModel> Create(CreateExecutorModel item)
        {
            var executor = _mapper.Map<Executor>(item);
            var result = _validator.Validate(executor);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            executor.CreationDate = DateTime.UtcNow;

            await AddPhotos(item, executor);
            await _repository.Create(executor);
            await _commitProvider.SaveAsync();
            await _userService.SetExecutorRole(item.UserId);


            return _mapper.Map<ExecutorViewModel>(executor);
        }

        public async Task Update(UpdateExecutorModel item)
        {
            var executor = _repository.GetAll().SingleOrDefault(o => o.Id == item.Id);
            if (executor == null)
            {
                throw new ValidationException("The executor doesn't exist");
            }

            var newExecutor = _mapper.Map<Executor>(item);

            var result = _validator.Validate(newExecutor);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await AddPhotos(item, newExecutor);
            _repository.Update(newExecutor);
            await _commitProvider.SaveAsync();
        }

        public async Task<ExecutorPage> GetPage(int pageNumber, int pageSize)
        {
            var executors = await _repository.GetAll()
                .Include(o => o.Photos)
                .Include(o => o.WorkType)
                .OrderByDescending(x => x.CreationDate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalCount = await _repository.GetAll().CountAsync();

            return new ExecutorPage
            {
                Executors = _mapper.Map<IEnumerable<ExecutorPageViewModel>>(executors),
                TotalCount = totalCount
            };
        }

        public async Task<ExecutorViewModel> Get(int id)
        {
            var executor = await _repository.GetAll()
                .Include(o => o.WorkType)
                .Include(o => o.Photos)
                .SingleOrDefaultAsync(o => o.Id == id);

            return executor == null ? null : _mapper.Map<ExecutorViewModel>(executor);
        }

        public Task GetExecutorRequests(int executorId)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            var executor = _repository.GetAll().SingleOrDefault(o => o.Id == id);
            if (executor == null)
            {
                throw new ValidationException("The executor doesn't exist");
            }

            await _repository.Delete(id);
            await _commitProvider.SaveAsync();
        }

        private async Task AddPhotos(CreateExecutorModel item, Executor executor)
        {
            if (item.Photos != null)
            {
                var photos = await _photoService.GetByIds(item.Photos);
                executor.Photos = new List<Photo>();
                foreach (var photo in photos)
                {
                    executor.Photos.Add(photo);
                }
            }
        }
    }
}
