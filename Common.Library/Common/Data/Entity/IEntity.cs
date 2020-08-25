namespace Common.Data.Entity
{
    /// <summary>
    /// Интерфейс базовой сущности
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        long Id { get; set; }
    }
}