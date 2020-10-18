namespace Common.Test.Base
{
    /// <summary>
    /// Fixture позволяет спрятать детали создания тестируемых классов.
    /// При этом позволяет настраивать создание объекта с помощью паттерна Builder.
    /// <remarks>
    /// Пример:
    /// new OrderFixture()
    ///     .WithPrice(1000)
    ///     .Create()
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">Тестируемый класс</typeparam>
    public interface IFixture<out T> where T : class
    {
        /// <summary>
        /// Создать минимальный валидный экземпляр класса
        /// </summary>
        /// <returns>Созданный экземпляр класса</returns>
        T Create();
    }
}
