﻿using System.Collections.Generic;
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
    public class WorkTypeService : IWorkService
    {
        private readonly IRepository<WorkType> _repository;
        private readonly ICommitProvider _commitProvider;
        private readonly IValidator<WorkTypeViewModel> _validator;
        private readonly IMapper _mapper;

        public WorkTypeService(
            IRepository<WorkType> repository,
            ICommitProvider commitProvider,
            IValidator<WorkTypeViewModel> validator,
            IMapper mapper)
        {
            _repository = repository;
            _commitProvider = commitProvider;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task Create(WorkTypeViewModel workType)
        {
            CheckWorkType(workType);
            await _repository.Create(new WorkType
            {
                Name = workType.Name
            });

            await _commitProvider.SaveAsync();
        }

        public async Task Update(WorkTypeViewModel workType)
        {
            CheckWorkType(workType);
            var type = _repository.GetAll().SingleOrDefault(o => o.Id == workType.Id);
            if (type == null)
            {
                throw new ValidationException("The work type doesn't exist");
            }

            type.Name = workType.Name;
            await _commitProvider.SaveAsync();
        }

        public async Task<IEnumerable<WorkTypeViewModel>> Get()
        {
            var types = await _repository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<WorkTypeViewModel>>(types);
        }

        public async Task Delete(int id)
        {
            var workType = _repository.GetAll().SingleOrDefault(o => o.Id == id);
            if (workType == null)
            {
                throw new ValidationException("The work type doesn't exist");
            }

            await _repository.Delete(id);
            await _commitProvider.SaveAsync();
        }

        private void CheckWorkType(WorkTypeViewModel workType)
        {
            var result = _validator.Validate(workType);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

    }
}
