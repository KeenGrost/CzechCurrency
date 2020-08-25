using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Data.Repository
{
    /// <summary>
    /// Интерфейс базового репозитория для сущности типа {TEntity}
    /// </summary>
    /// <typeparam name="TEntity">Cущность доменной модели</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Найти сущность по ключу (или составному ключу)
        /// </summary>
        /// <param name="id">Ключ (или составной ключ)</param>
        /// <returns>Найденная сущность или null если сущность не найдена</returns>
        [ItemCanBeNull]
        Task<TEntity> Find(params object[] id);

        ///// <summary>
        ///// Найти сущности по коллекции ключей
        ///// </summary>
        ///// <param name="ids">Коллекция ключей</param>
        ///// <param name="noTracking">Возвращаемые сущности не привязаны к сессии</param>
        ///// <returns>Коллекция найденных сущностей</returns>
        //[ItemNotNull]
        //Task<TEntity[]> FindAll([NotNull] IEnumerable<long> ids, bool noTracking = false);

        /// <summary>
        /// Получить все сущности
        /// </summary>
        /// <param name="noTracking">Возвращаемые сущности не привязаны к сессии</param>
        /// <returns>Все сущности</returns>
        [ItemNotNull]
        Task<TEntity[]> GetAll(bool noTracking = false);

        /// <summary>
        /// Получить все сущности по фильтру
        /// </summary>
        /// <param name="filter">Условие отбора сущностей</param>
        /// <param name="noTracking">Возвращаемые сущности не привязаны к сессии</param>
        /// <returns>Все сущности</returns>
        [ItemNotNull]
        Task<TEntity[]> GetAll([NotNull] Expression<Func<TEntity, bool>> filter, bool noTracking = false);

        /// <summary>
        /// Добавить сущность к контексту
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        /// <returns>Количество добавленных объектов</returns>
        Task<int> Add([NotNull] TEntity entity);

        /// <summary>
        /// Добавить заданную коллекцию сущностей к контексту
        /// </summary>
        /// <param name="entities">Коллекция сущностей для добавления</param>
        /// <returns>Количество добавленных объектов</returns>
        Task<int> AddRange([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// Обновить сущность в контексте
        /// </summary>
        /// <param name="entity">Обновляемая сущность</param>
        /// <returns>Количество обновлённых объектов</returns>
        Task<int> Update([NotNull] TEntity entity);

        /// <summary>
        /// Обновить заданную коллекцию сущностей в контексте
        /// </summary>
        /// <param name="entities">Коллекция сущностей для добавления</param>
        /// <returns>Количество обновлённых объектов</returns>
        Task<int> UpdateRange([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// Удалить сущности из контекста
        /// </summary>
        /// <param name="id">Ключ (или составной ключ)</param>
        /// <returns>Количество удалённых объектов</returns>
        Task<int> Delete(params object[] id);

        /// <summary>
        /// Удалить сущности из контекста по фильтру
        /// </summary>
        /// <param name="filter">Условие отбора сущностей для удаления</param>
        /// <returns>Количество удалённых объектов</returns>
        Task<int> Delete([NotNull] Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Удалить сущности из контекста
        /// </summary>
        /// <param name="entity">Удаляемая сущность</param>
        /// <returns>Количество удалённых объектов</returns>
        Task<int> Delete([NotNull] TEntity entity);

        /// <summary>
        /// Удалить заданную коллекцию сущностей из контекста
        /// </summary>
        /// <param name="entities">Коллекция сущностей для удаления</param>
        /// <returns>Количество удалённых объектов</returns>
        Task<int> DeleteRange([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// Получение количества сущностей в контексте
        /// </summary>
        /// <param name="filter">Условие отбора сущностей</param>
        /// <returns>Количество объектов</returns>
        Task<int> Count([CanBeNull] Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Сохранить изменения в контексте
        /// </summary>
        /// <returns>Количество сохранённых объектов</returns>
        Task<int> SaveChanges();
    }
}
