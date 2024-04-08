using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Shared.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Core.v1.Generic
{
    public abstract class GenericCore<T, Q, C> where T : class, new() where Q : class, new() where C : class, new()
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly ILogger<T> _logger;
        protected readonly ErrorHandler<T> _errors;
        private readonly string _coreName;
        public int UserId { get; set; }

        public GenericCore(string coreName, IGenericRepository<T> repository, ILogger<T> logger)
        {
            _coreName = coreName;
            _repository = repository;
            _logger = logger;
            _errors = new ErrorHandler<T>(logger);
        }

        public virtual async Task<ResponseService<IEnumerable<Q>>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                if (result is null || !result.Any()) return new ResponseService<IEnumerable<Q>>(false, $"No Records found", HttpStatusCode.OK, [], 0);

                var records = result.Count();
                var content = result.Select(r => ExtensionMethods.MapperTo<T, Q>(r)).ToList();
                return new ResponseService<IEnumerable<Q>>(false, $"{records} records found", HttpStatusCode.OK, content, records);
            }
            catch (Exception ex)
            {
                return _errors.Error<IEnumerable<Q>>(ex, $"{_coreName}.GetAllAsync", []);
            }
        }

        public virtual async Task<ResponseService<Q>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                if (result is null) return new ResponseService<Q>(false, $"No record found (Id:{id})", HttpStatusCode.OK, new(), 0);

                var content = ExtensionMethods.MapperTo<T, Q>(result);
                return new ResponseService<Q>(false, $"Id: {id}, Record found", HttpStatusCode.OK, content, 1);
            }
            catch (Exception ex)
            {
                return _errors.Error<Q>(ex, $"{_coreName}.GetByIdAsync", new ());
            }
        }

        public virtual async Task<ResponseService<IEnumerable<Q>>> GetByFilterAsync(Expression<Func<T, bool>> filterExpression = null!)
        {
            try
            {
                var result = await _repository.GetByFilterAsync(filterExpression);
                if (result is null || !result.Any()) return new ResponseService<IEnumerable<Q>>(false, $"No Records found", HttpStatusCode.OK, [], 0);

                var records = result.Count();
                var content = result.Select(r => ExtensionMethods.MapperTo<T, Q>(r)).ToList();
                return new ResponseService<IEnumerable<Q>>(false, $"{records} records found", HttpStatusCode.OK, content, records);
            }
            catch (Exception ex)
            {
                return _errors.Error<IEnumerable<Q>>(ex, $"GetByFilterAsync.{_coreName}", []);
            }
        }

        public virtual async Task<ResponseService<Q>> AddAsync(C entity)
        {
            try
            {
                var newEntity = new T();
                newEntity = ExtensionMethods.MapperTo<C, T>(entity);

                var response = await _repository.AddAsync(newEntity);
                if (!response.Added) return _errors.Warning<Q>("Record not added", "AddAsync", new());

                var responseDto = ExtensionMethods.MapperTo<T, Q>(response.entity);
                return new ResponseService<Q>(false, "Record Added Correctly", HttpStatusCode.Created, responseDto);
            }
            catch (Exception ex)
            {
                return _errors.Error<Q>(ex, $"AddAsync.{_coreName}", new Q());
            }
        }

        public virtual async Task<ResponseService<bool>> UpdateAsync(C entity, int id)
        {
            try
            {
                var current = await _repository.GetByIdAsync(id);
                if (current == null) return _errors.Warning<bool>($"Record not found Id: {id}", "AddAsync", false);

                current = ExtensionMethods.MapperTo<C, T>(entity);
                var response = await _repository.UpdateAsync(current, id);
                return new ResponseService<bool>(false, response ? "Record Updated Correctly" : "Record Not Updated", HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return _errors.Error<bool>(ex, $"UpdateAsync.{_coreName}", false);
            }

        }

    }
}
