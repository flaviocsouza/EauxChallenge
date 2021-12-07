using Business.Interfaces.Repositories;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : Entity , new()
    {
        protected readonly EauxDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(EauxDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> List() => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<TEntity> FindById(Guid id) => await _dbSet.AsNoTracking().FirstAsync(t => t.Id == id);

        public async Task Insert(TEntity entity)
        {
           await _dbSet.AddAsync(entity);
           await SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var entity = new TEntity
            {
                Id = id
            };
            _dbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
