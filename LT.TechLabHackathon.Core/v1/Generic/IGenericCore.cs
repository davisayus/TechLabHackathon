using LT.TechLabHackathon.Shared.Helpers;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Core.v1.Generic
{
    public interface IGenericCore<T, Q, C> where T : class, new() where Q : class, new() where C : class, new()
    {
        Task<ResponseService<IEnumerable<Q>>> GetAllAsync();
        Task<ResponseService<Q>> GetByIdAsync(int id);
        Task<ResponseService<IEnumerable<Q>>> GetByFilterAsync(Expression<Func<T, bool>> filterExpression = null!);
        Task<ResponseService<Q>> AddAsync(C entity);
        Task<ResponseService<bool>> UpdateAsync(C entity, int id);
    }
}
