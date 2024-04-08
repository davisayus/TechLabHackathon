using LT.TechLabHackathon.DataAccess.SqlServerContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.DataAccess.Repositories.Generic
{
    public class GenericRepository<T>(SqlContext context) where T : class, new()
    {
        protected readonly SqlContext _context = context;

        public virtual async Task<(T, bool)> AddAsync(T entity)
        {
            var addedEntity = _context.Add<T>(entity).Entity;
            int recordAffected = await _context.SaveChangesAsync();
            return (addedEntity, recordAffected != 0);
        }
        public virtual async Task<bool> UpdateAsync(T entity, int id)
        {
            _context.Set<T>().Update(entity);
            int recordAffected = await _context.SaveChangesAsync();
            return (recordAffected == 1);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result;
        }
        public virtual async Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filterExpression = null!)
        {
            var result = await _context.Set<T>().Where(filterExpression).ToListAsync();
            return result;
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result!;
        }

        public virtual async Task<bool> AddArrayAsync(List<T> entities)
        {
            await _context.AddRangeAsync(entities);
            var response = await _context.SaveChangesAsync();
            return (response == entities.Count);
        }
    }

}
