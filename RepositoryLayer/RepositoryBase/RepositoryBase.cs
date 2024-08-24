using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;

namespace RepositoryLayer.RepositoryBase
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext _context;
        public RepositoryBase(ApplicationDbContext repositoryContext)
        => _context = repositoryContext;

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? _context.Set<T>().AsNoTracking()
            : _context.Set<T>();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
            => !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() :
                                _context.Set<T>().Where(expression);
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
           await _context.Set<T>().AnyAsync(predicate);

    }
}
