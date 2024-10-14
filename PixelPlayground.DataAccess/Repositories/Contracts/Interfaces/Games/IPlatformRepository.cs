using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;


/// <summary>
/// Интерфейс для работы с платформами.
/// </summary>
public interface IPlatformRepository
{
    /// <summary>
    /// Проверяет, существует ли платформа по её имени.
    /// </summary>
    /// <param name="platformName">Название платформы.</param>
    /// <returns>true, если платформа существует; иначе false.</returns>
    Task<bool> PlatformExistsAsync(string platformName);

    /// <summary>
    /// Создает новую платформу.
    /// </summary>
    /// <param name="platformName">Название платформы, которую нужно создать.</param>
    /// <returns>true, если платформа успешно создана; иначе false.</returns>
    Task<bool> CreatePlatformAsync(string platformName);

    /// <summary>
    /// Получает все платформы.
    /// </summary>
    /// <returns>Список всех платформ.</returns>
    Task<IEnumerable<PlatformEntity>> GetAllPlatformsAsync();

    /// <summary>
    /// Получает идентификатор платформы по её имени.
    /// </summary>
    /// <param name="platformName">Название платформы.</param>
    /// <returns>Идентификатор платформы, если платформа найдена; иначе null.</returns>
    Task<Guid?> GetIdPlatformByNameAsync(string platformName);

    /// <summary>
    /// Получает платформу по её идентификатору.
    /// </summary>
    /// <param name="platformId">Идентификатор платформы.</param>
    /// <returns>Объект платформы, если платформа найдена; иначе null.</returns>
    Task<PlatformEntity?> GetPlatformByIdAsync(Guid platformId);

    /// <summary>
    /// Обновляет существующую платформу.
    /// </summary>
    /// <param name="platformId">Идентификатор платформы.</param>
    /// <param name="newPlatformName">Новое название платформы.</param>
    /// <returns>true, если платформа успешно обновлена; иначе false.</returns>
    Task<bool> UpdatePlatformAsync(Guid platformId, string newPlatformName);

    /// <summary>
    /// Удаляет платформу по её идентификатору (мягкое удаление).
    /// </summary>
    /// <param name="platformId">Идентификатор платформы.</param>
    /// <returns>true, если платформа успешно удалена; иначе false.</returns>
    Task<bool> DeletePlatformAsync(Guid platformId);
}