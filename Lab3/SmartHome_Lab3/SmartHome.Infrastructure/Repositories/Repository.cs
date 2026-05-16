using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHome.Common;
using SmartHome.Infrastructure.Data;

namespace SmartHome.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SmartHomeContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(SmartHomeContext context) { _context = context; _dbSet = _context.Set<T>(); }

        public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) { await _dbSet.AddAsync(entity); await _context.SaveChangesAsync(); }
        public async Task Update(T entity) { _dbSet.Update(entity); await _context.SaveChangesAsync(); }
        public async Task Delete(T entity) { _dbSet.Remove(entity); await _context.SaveChangesAsync(); }
    }
}