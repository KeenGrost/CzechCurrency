using System;

namespace Common.Pagination
{
    /// <summary>
    /// Базовый класс для работы с постраничным выводом
    /// </summary>
    public class PaginationResult
    {
        // TODO: после выхода .NET Core 5.0 поставить protected setter. Не работала сериализация
        // ReSharper disable once InconsistentNaming
        public PaginationMetadata PageInfo { get; set; }
    }

    /// <summary>
    /// Базовый ответ с постраничным выводом
    /// </summary>
    public class PaginationResult<TEntity> : PaginationResult
    {
        /// <summary>
        /// Коллекция сущностей
        /// </summary>
        public TEntity[] Items { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">Коллекция сущностей</param>
        /// <param name="pageInfo">Метаданные</param>
        public PaginationResult(TEntity[] items, PaginationMetadata pageInfo)
        {
            Items = items;
            PageInfo = pageInfo;
        }

        /// <summary>
        /// Создать новый результат на основе метаданных
        /// </summary>
        /// <typeparam name="TREntity">Новый тип сущности</typeparam>
        /// <param name="items">Коллекция сущностей</param>
        public PaginationResult<TREntity> Create<TREntity>(TREntity[] items)
        {
            return new PaginationResult<TREntity>(items, PageInfo);
        }

        /// <summary>
        /// Создать новый результат на основе метаданных
        /// </summary>
        /// <typeparam name="TResult">Новый тип сущности</typeparam>
        /// <param name="lambda">Функция конвертации коллекции текущей сущности в новую</param>
        public PaginationResult<TResult> Create<TResult>(Func<TEntity[], TResult[]> lambda)
        {
            return new PaginationResult<TResult>(lambda(Items), PageInfo);
        }

        /// <summary>
        /// Создание пустого результата из постраничного запроса
        /// </summary>
        /// <param name="paginationQuery">Постраничный запрос</param>
        /// <returns></returns>
        public static PaginationResult<TEntity> CreateEmpty(PaginationQuery paginationQuery)
        {
            var pageInfo = paginationQuery.GetPaginationMetadata(0);
            return new PaginationResult<TEntity>(new TEntity[0], pageInfo);
        }
    }
}
