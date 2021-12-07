

using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        public Task<IEnumerable<TEntity>> List(); 
        public Task<TEntity> FindById(Guid id);
        public Task Insert(TEntity entity);
        public Task Update(TEntity entity);
        public Task Delete(Guid id);
        public Task SaveChanges();
        public void Dispose();
    }
}
