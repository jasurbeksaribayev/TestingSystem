using AspnetApp.Data.DbContexts;
using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Data.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Auditable
    {
        private readonly AspnetAppDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AspnetAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public async ValueTask<T> CreateAsync(T entity)
        => (await dbSet.AddAsync(entity)).Entity;

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var existUser = await GetAsync(e => e.Id == id);

            if (existUser == null)
                return false;

            dbSet.Remove(existUser);
            return true;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
                return dbSet;

            return dbSet.Where(expression);
        }
        public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression)
        => await GetAll(expression).FirstOrDefaultAsync();

        public async ValueTask SaveChangesasync()
        => await dbContext.SaveChangesAsync();

        public T Update(T entity)
        => dbSet.Update(entity).Entity;
    }
}
