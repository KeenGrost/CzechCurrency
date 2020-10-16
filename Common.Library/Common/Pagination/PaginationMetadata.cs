using JetBrains.Annotations;
using System;

namespace Common.Pagination
{
    // TODO: после выхода .NET Core 5.0 поставить readonly setter и протестировать. Не работала сериализация

    /// <summary>
    /// Метаданные страничного представления списков
    /// </summary>
    public class PaginationMetadata
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int PageSize { get; set; }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Общее количество элементов
        /// </summary>
        public long TotalItem { get; set; }

        /// <summary>
        /// Строка поиска
        /// <remarks> Опционально</remarks>
        /// </summary>
        [CanBeNull]
        public string Filter { get; set; }

        /// <summary>
        /// Поиск по полю
        /// <remarks> Опционально</remarks>
        /// </summary>
        [CanBeNull]
        public string FilterBy { get; set; }

        /// <summary>
        /// Сортировка по полю
        /// <remarks> Опционально</remarks>
        /// </summary>
        [CanBeNull]
        public string OrderBy { get; set; }

        /// <summary>
        /// Порядок сортировки
        /// <remarks> Опционально</remarks>
        /// </summary>
        [CanBeNull]
        public SortDirection? Sort { get; set; }

        /// <summary>
        /// Открытый конструктор
        /// </summary>
        public PaginationMetadata(int page, int pageSize, long totalItem, [CanBeNull] string filter = null,
            [CanBeNull] string filterBy = null, [CanBeNull] string orderBy = null, [CanBeNull] SortDirection? sort = null)
        {
            Page = page;
            PageSize = pageSize;
            TotalItem = totalItem;
            Filter = filter;
            FilterBy = filterBy;
            OrderBy = orderBy;
            Sort = sort;
            TotalPage = (int)Math.Ceiling((double)TotalItem / PageSize);
        }

        /// <summary>
        /// Статический фабричный метод для создания экземпляра PaginationMetadata
        /// </summary>
        /// <returns>PaginationMetadata</returns>
        public static PaginationMetadata Create(int page, int pageSize, long totalItem, [CanBeNull] string filter = null,
            [CanBeNull] string filterBy = null, [CanBeNull] string orderBy = null, [CanBeNull] SortDirection? sort = null)
        {
            return new PaginationMetadata(page, pageSize, totalItem, filter, filterBy, orderBy, sort);
        }
    }
}
