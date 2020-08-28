using Common.Data.Repository;
using System.Threading.Tasks;

namespace Common.Service
{
    /// <summary>
    /// Базовый сервис для сущности типа {TEntity}
    /// </summary>
    /// <typeparam name="TEntity">Сущность доменной модели</typeparam>
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;

        protected BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<TEntity[]> GetAll()
        {
            return await _repository.GetAll(noTracking: true);
        }

        public virtual async Task<TEntity> GetById(long id)
        {
            return await _repository.Find(id);
        }

        public virtual async Task<int> Add(TEntity entity)
        {
            return await _repository.Add(entity);
        }

        public virtual async Task<int> Update(TEntity entity)
        {
            return await _repository.Update(entity);
        }

        public virtual async Task<int> Delete(long id)
        {
            return await _repository.Delete(id);
        }

        public virtual async Task<bool> Exists(long id)
        {
            return await _repository.Find(id) != null;
        }

        public virtual async Task<int> SaveChanges()
        {
            return await _repository.SaveChanges();
        }
    }
}
