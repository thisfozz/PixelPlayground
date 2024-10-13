using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface ISystemRequirementRepository
{
    // Проверка существуют ли системные требования к конкретной игре
    Task<bool> SystemRequirementsExistsAsync(Guid gameId);

    // Создание новых системных требований для игры
    Task<bool> CreateSystemRequirementsAsync(SystemRequirementEntity systemRequirement);

    // Получение системных требований по ID игры
    Task<SystemRequirementEntity?> GetSystemRequirementsByGameIdAsync(Guid gameId);

    // Обновление системных требований для игры
    Task<bool> UpdateSystemRequirementsAsync(SystemRequirementEntity systemRequirement);

    // Удаление системных требований по ID игры
    Task<bool> DeleteSystemRequirementsAsync(Guid gameId);
}