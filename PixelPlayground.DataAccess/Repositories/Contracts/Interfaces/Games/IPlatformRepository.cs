using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface IPlatformRepository
{
    // Проверка, существует ли платформа по её имени
    Task<bool> PlatformExistsAsync(string platformName);

    // Создание новой платформы
    Task<bool> CreatePlatformAsync(string platformName);

    // Получение всех платформ
    Task<IEnumerable<PlatformEntity>> GetAllPlatformsAsync();

    // Получение ID платформы по её имени
    Task<Guid?> GetIdPlatformByNameAsync(string platformName);

    // Получение платформы по её ID
    Task<PlatformEntity?> GetPlatformByIdAsync(Guid platformId);

    // Обновление платформы
    Task<bool> UpdatePlatformAsync(Guid platformId, string newPlatformName);

    // Удаление платформы по её ID (мягкое удаление)
    Task<bool> DeletePlatformAsync(Guid platformId);
}