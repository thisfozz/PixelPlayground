using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface IDeveloperRepository
{
    // Проверка, существует ли разработчик по его имени
    Task<bool> DeveloperExistsAsync(string developerName);

    // Создание нового разработчика
    Task<bool> CreateDeveloperAsync(string developerName);

    // Получение всех разработчиков
    Task<IEnumerable<DeveloperEntity>> GetAllDevelopersAsync();

    // Получение ID разработчика по его имени
    Task<Guid?> GetIdDeveloperByNameAsync(string developerName);

    // Получение разработчика по его ID
    Task<DeveloperEntity?> GetDeveloperByIdAsync(Guid developerId);

    // Обновление разработчика
    Task<bool> UpdateDeveloperAsync(Guid developerId, string newDeveloperName);

    // Удаление разработчика по его ID (мягкое удаление)
    Task<bool> DeleteDeveloperAsync(Guid developerId);
}