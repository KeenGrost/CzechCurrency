using System.Threading.Tasks;

namespace Common.Utility
{
    public interface IUtilityTask
    {
        /// <summary>
        /// Выполнение задачи
        /// </summary>
        /// <returns></returns>
        Task Execute();
    }
}
