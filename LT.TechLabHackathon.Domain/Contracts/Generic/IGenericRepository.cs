using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Contracts.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<(T entity, bool Added)> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity, int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filterExpression = null!);
    }
}
