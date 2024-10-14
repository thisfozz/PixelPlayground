using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;


/// <summary>
/// Интерфейс для работы с системными требованиями игр.
/// </summary>
public interface ISystemRequirementRepository
{
    /// <summary>
    /// Проверяет, существуют ли системные требования к конкретной игре.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>true, если системные требования существуют; иначе false.</returns>
    Task<bool> SystemRequirementsExistsAsync(Guid gameId);

    /// <summary>
    /// Создает новые системные требования для игры.
    /// </summary>
    /// <param name="systemRequirement">Объект системных требований, который нужно создать.</param>
    /// <returns>true, если системные требования успешно созданы; иначе false.</returns>
    Task<bool> CreateSystemRequirementsAsync(SystemRequirementEntity systemRequirement);

    /// <summary>
    /// Получает системные требования по идентификатору игры.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Объект системных требований, если они найдены; иначе null.</returns>
    Task<SystemRequirementEntity?> GetSystemRequirementsByGameIdAsync(Guid gameId);

    /// <summary>
    /// Обновляет системные требования для игры.
    /// </summary>
    /// <param name="systemRequirement">Объект с новыми системными требованиями.</param>
    /// <returns>true, если системные требования успешно обновлены; иначе false.</returns>
    Task<bool> UpdateSystemRequirementsAsync(SystemRequirementEntity systemRequirement);

    /// <summary>
    /// Удаляет системные требования по идентификатору игры.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>true, если системные требования успешно удалены; иначе false.</returns>
    Task<bool> DeleteSystemRequirementsAsync(Guid gameId);
}