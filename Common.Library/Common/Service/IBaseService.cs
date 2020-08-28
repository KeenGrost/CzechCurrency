using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Common.Service
{
    /// <summary>
    /// Интерфейс базового сервиса для сущности типа {TEntity}
    /// </summary>
    /// <typeparam name="TEntity">Cущность доменной модели</typeparam>
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получить все сущности
        /// <remarks>
        /// Запрос с отключением отслеживания состояния
        /// </remarks>
        /// </summary>
        /// <returns>Список сущностей</returns>
        [ItemNotNull]
        Task<TEntity[]> GetAll();

        /// <summary>
        /// Найти сущность по ключу
        /// </summary>
        /// <param name="id">Ключ</param>
        /// <returns>Найденная сущность или null если сущность не найдена</returns>
        [ItemCanBeNull]
        Task<TEntity> GetById(long id);

        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        /// <returns>Количество добавленных объектов</returns>
        Task<int> Add([NotNull] TEntity entity);

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity">Обновляемая сущность</param>
        /// <returns>Количество обновлённых объектов</returns>
        Task<int> Update([NotNull] TEntity entity);

        /// <summary>
        /// Удалить сущности
        /// </summary>
        /// <param name="id">Ключ</param>
        /// <returns>Количество удалённых объектов</returns>
        Task<int> Delete(long id);

        /// <summary>
        /// Проверка существования сущности
        /// </summary>
        /// <param name="id">Ключ</param>
        /// <returns>Существует ли сущность</returns>
        Task<bool> Exists(long id);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <returns>Количество сохранённых объектов</returns>
        Task<int> SaveChanges();
    }
}
