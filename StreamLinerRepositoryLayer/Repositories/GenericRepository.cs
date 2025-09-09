using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerEntitiesLayer.Entities.IEntity;
using StreamLinerRepositoryLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerRepositoryLayer.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
      
         
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
      
        public async Task<T?> GetByNameAsync(string name)
        {
            var property = typeof(T).GetProperty("Name");
            if (property == null)
                throw new InvalidOperationException($"{typeof(T).Name} does not contain a property named 'Name'");

            return await _dbSet
                .AsQueryable()
                .FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task<T> GetFindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize)
        {
            var total = await _dbSet.CountAsync();
            var items = await _dbSet
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        
    }
}
