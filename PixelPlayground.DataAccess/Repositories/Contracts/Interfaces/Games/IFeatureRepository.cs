using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;


/// <summary>
/// Интерфейс для работы с фичами.
/// </summary>
public interface IFeatureRepository
{
    /// <summary>
    /// Проверяет, существует ли фича по её имени.
    /// </summary>
    /// <param name="featureName">Имя фичи.</param>
    /// <returns>true, если фича существует; иначе false.</returns>
    Task<bool> FeatureExistsAsync(string featureName);

    /// <summary>
    /// Создает новую фичу.
    /// </summary>
    /// <param name="featureName">Имя фичи, которую нужно создать.</param>
    /// <returns>true, если фича успешно создана; иначе false.</returns>
    Task<bool> CreateFeatureAsync(string featureName);

    /// <summary>
    /// Получает все фичи.
    /// </summary>
    /// <returns>Список всех фич.</returns>
    Task<IEnumerable<FeatureEntity>> GetAllFeaturesAsync();

    /// <summary>
    /// Получает идентификатор фичи по её имени.
    /// </summary>
    /// <param name="featureName">Имя фичи.</param>
    /// <returns>ID фичи или null, если фича не найдена.</returns>
    Task<Guid?> GetIdFeatureByNameAsync(string featureName);

    /// <summary>
    /// Получает фичу по её идентификатору.
    /// </summary>
    /// <param name="featureId">Идентификатор фичи.</param>
    /// <returns>Объект фичи или null, если фича не найдена.</returns>
    Task<FeatureEntity?> GetFeatureByIdAsync(Guid featureId);

    /// <summary>
    /// Обновляет фичу.
    /// </summary>
    /// <param name="featureId">Идентификатор фичи, которую нужно обновить.</param>
    /// <param name="newFeatureName">Новое имя фичи.</param>
    /// <returns>true, если фича успешно обновлена; иначе false.</returns>
    Task<bool> UpdateFeatureAsync(Guid featureId, string newFeatureName);

    /// <summary>
    /// Удаляет фичу по её идентификатору (мягкое удаление).
    /// </summary>
    /// <param name="featureId">Идентификатор фичи, которую нужно удалить.</param>
    /// <returns>true, если фича успешно удалена; иначе false.</returns>
    Task<bool> DeleteFeatureAsync(Guid featureId);
}