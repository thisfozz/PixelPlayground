using DataAccess.Entities.Games;
using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface IGameRepository
{
    // Создание игры
    Task<bool> CreateGameAsync(GameEntity game);

    // Получение всех игр
    Task<IEnumerable<GameEntity>> GetAllGamesAsync();

    // Получение игры по ее ID
    Task<GameEntity?> GetGameByIdAsync(Guid gameId);

    // Получение игры по названию
    Task<GameEntity?> GetGameByNameAsync(string name);

    // Получение название игры по ID. Создано для простоты и легкости запроса.
    Task<string?> GetTitleByGameId(Guid gameId);

    // Получение разработчика игры
    Task<DeveloperEntity?> GetDeveloperByGameIdAsync(Guid gameId);

    // Получение издателя игры
    Task<PublisherEntity?> GetPublisherByGameIdAsync(Guid gameId);

    // Получение всех фич для конкретной игры по ID игры
    Task<IEnumerable<FeatureEntity?>> GetFeaturesByGameIdAsync(Guid gameId);

    // Получение всех изображений для конкретной игры по ID игры
    Task<IEnumerable<GameImageEntity?>> GetImagesByGameIdAsync(Guid gameId);

    // Получение всех жанров для конкретной игры по ID игры
    Task<IEnumerable<GenreEntity?>> GetGenresByGameIdAsync(Guid gameId);

    // Получение всех платформ для конкретной игры по ID игры
    Task<IEnumerable<PlatformEntity?>> GetPlatformsByGameIdAsync(Guid gameId);

    // Получение системных требований для конкретной игры по ID игры
    Task<IEnumerable<SystemRequirementEntity>> GetSystemRequirementsByGameIdAsync(Guid gameId);

    // Получение всех ревью для конкретной игры по ID игры
    Task<IEnumerable<ReviewEntity?>> GetReviewsByGameIdAsync(Guid gameId);

    // Обновление рейтинга игры на основе отзывов
    Task<bool> UpdateGameRatingAsync(Guid gameId, uint rating);

    Task<bool> UpdateGameAsync(Guid gameId, GameEntity game, List<Guid> genresToRemove, List<Guid> platformsToRemove, List<Guid> featuresToRemove);

    // Добавление платформы в игру
    Task<bool> AddPlatformToGameAsync(Guid gameId, PlatformEntity platform);

    // Удаление платформы из игры
    Task<bool> RemovePlatformFromGameAsync(Guid gameId, Guid platformId);

    // Добавление жанра в игру
    Task<bool> AddGenreToGameAsync(Guid gameId, GenreEntity genre);

    // Удаление жанра из игры
    Task<bool> RemoveGenreFromGameAsync(Guid gameId, Guid genreId);

    // Добавление фичи в игру
    Task<bool> AddFeatureToGameAsync(Guid gameId, FeatureEntity feature);

    // Удаление фичи из игры
    Task<bool> RemoveFeatureFromGameAsync(Guid gameId, Guid featureId);

    // Удаление игры по её ID
    Task<bool> DeleteGameAsync(Guid gameId);
}