using StreamLinerEntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerRepositoryLayer.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
       
        public IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByNameAsync(string name);

        Task AddAsync(T entity);
        void Update(T entity);
         void Delete(T entity);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetFindAsync(Expression<Func<T, bool>> predicate);

        Task<bool> SaveChangesAsync();
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize);

        Task<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
         
    }
}
