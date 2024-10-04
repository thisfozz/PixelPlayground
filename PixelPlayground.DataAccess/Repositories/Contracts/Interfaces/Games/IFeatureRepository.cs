using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface IFeatureRepository
{
    // Проверка, существует ли фича по её имени
    Task<bool> FeatureExistsAsync(string featureName);

    // Создание новой фичи
    Task<bool> CreateFeatureAsync(string featureName);

    // Получение всех фичи
    Task<IEnumerable<FeatureEntity>> GetAllFeaturesAsync();

    // Получение ID фичи игры по его имени
    Task<Guid?> GetIdDeveloperByNameAsync(string featureName);

    // Получение фичи по её ID
    Task<FeatureEntity?> GetFeatureByIdAsync(Guid featureId);

    // Обновление фичи
    Task<bool> UpdateFeatureAsync(Guid featureId, string newFeatureName);

    // Удаление фичи по её ID (мягкое удаление)
    Task<bool> DeleteFeatureAsync(Guid featureId);
}