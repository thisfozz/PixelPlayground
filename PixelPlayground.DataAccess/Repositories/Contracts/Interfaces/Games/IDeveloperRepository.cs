using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;


/// <summary>
/// Интерфейс для работы с разработчиками.
/// </summary>
public interface IDeveloperRepository
{
    /// <summary>
    /// Проверяет, существует ли разработчик по его имени.
    /// </summary>
    /// <param name="developerName">Имя разработчика.</param>
    /// <returns>true, если разработчик существует; иначе false.</returns>
    Task<bool> DeveloperExistsAsync(string developerName);

    /// <summary>
    /// Создает нового разработчика.
    /// </summary>
    /// <param name="developerName">Имя разработчика, который нужно создать.</param>
    /// <returns>true, если разработчик успешно создан; иначе false.</returns>
    Task<bool> CreateDeveloperAsync(string developerName);

    /// <summary>
    /// Получает всех разработчиков.
    /// </summary>
    /// <returns>Список всех разработчиков.</returns>
    Task<IEnumerable<DeveloperEntity>> GetAllDevelopersAsync();

    /// <summary>
    /// Получает идентификатор разработчика по его имени.
    /// </summary>
    /// <param name="developerName">Имя разработчика.</param>
    /// <returns>ID разработчика или null, если разработчик не найден.</returns>
    Task<Guid?> GetIdDeveloperByNameAsync(string developerName);

    /// <summary>
    /// Получает разработчика по его идентификатору.
    /// </summary>
    /// <param name="developerId">Идентификатор разработчика.</param>
    /// <returns>Объект разработчика или null, если разработчик не найден.</returns>
    Task<DeveloperEntity?> GetDeveloperByIdAsync(Guid developerId);

    /// <summary>
    /// Обновляет разработчика.
    /// </summary>
    /// <param name="developerId">Идентификатор разработчика, которого нужно обновить.</param>
    /// <param name="newDeveloperName">Новое имя разработчика.</param>
    /// <returns>true, если разработчик успешно обновлен; иначе false.</returns>
    Task<bool> UpdateDeveloperAsync(Guid developerId, string newDeveloperName);

    /// <summary>
    /// Удаляет разработчика по его идентификатору.
    /// </summary>
    /// <param name="developerId">Идентификатор разработчика, которого нужно удалить.</param>
    /// <returns>true, если разработчик успешно удален; иначе false.</returns>
    Task<bool> DeleteDeveloperAsync(Guid developerId);
}