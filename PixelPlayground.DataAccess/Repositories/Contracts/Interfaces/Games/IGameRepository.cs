using DataAccess.Entities.Games;
using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;


/// <summary>
/// Интерфейс для работы с играми.
/// </summary>
public interface IGameRepository
{
    /// <summary>
    /// Создает новую игру.
    /// </summary>
    /// <param name="game">Объект игры, который нужно создать.</param>
    /// <returns>true, если игра успешно создана; иначе false.</returns>
    Task<bool> CreateGameAsync(GameEntity game);

    /// <summary>
    /// Получает все игры.
    /// </summary>
    /// <returns>Список всех игр.</returns>
    Task<IEnumerable<GameEntity>> GetAllGamesAsync();

    /// <summary>
    /// Получает игру по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Объект игры, если игра найдена; иначе null.</returns>
    Task<GameEntity?> GetGameByIdAsync(Guid gameId);

    /// <summary>
    /// Получает игру по её названию.
    /// </summary>
    /// <param name="name">Название игры.</param>
    /// <returns>Объект игры, если игра найдена; иначе null.</returns>
    Task<GameEntity?> GetGameByNameAsync(string name);

    /// <summary>
    /// Получает название игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Название игры, если игра найдена; иначе null.</returns>
    Task<string?> GetTitleByGameId(Guid gameId);

    /// <summary>
    /// Получает разработчика игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Объект разработчика, если игра найдена; иначе null.</returns>
    Task<DeveloperEntity?> GetDeveloperByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает издателя игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Объект издателя, если игра найдена; иначе null.</returns>
    Task<PublisherEntity?> GetPublisherByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает все фичи для конкретной игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Список фич игры.</returns>
    Task<IEnumerable<FeatureEntity?>> GetFeaturesByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает все изображения для конкретной игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Список изображений игры.</returns>
    Task<IEnumerable<GameImageEntity?>> GetImagesByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает все жанры для конкретной игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Список жанров игры.</returns>
    Task<IEnumerable<GenreEntity?>> GetGenresByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает все платформы для конкретной игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Список платформ игры.</returns>
    Task<IEnumerable<PlatformEntity?>> GetPlatformsByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает системные требования для конкретной игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Список системных требований игры.</returns>
    Task<IEnumerable<SystemRequirementEntity>> GetSystemRequirementsByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает все ревью для конкретной игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Список ревью игры.</returns>
    Task<IEnumerable<ReviewEntity?>> GetReviewsByGameIdAsync(Guid gameId);

    /// <summary>
    /// Обновляет рейтинг игры на основе отзывов.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="rating">Новое значение рейтинга.</param>
    /// <returns>true, если рейтинг успешно обновлён; иначе false.</returns>
    Task<bool> UpdateGameRatingAsync(Guid gameId, uint rating);

    /// <summary>
    /// Обновляет существующую игру.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="game">Объект игры с обновлёнными данными.</param>
    /// <returns>true, если игра успешно обновлена; иначе false.</returns>
    Task<bool> UpdateGameAsync(Guid gameId, GameEntity game);

    /// <summary>
    /// Добавляет платформу в игру.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="platformId">Идентификатор платформы.</param>
    /// <returns>true, если платформа успешно добавлена; иначе false.</returns>
    Task<bool> AddPlatformToGameAsync(Guid gameId, Guid platformId);

    /// <summary>
    /// Удаляет платформу из игры.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="platformId">Идентификатор платформы.</param>
    /// <returns>true, если платформа успешно удалена; иначе false.</returns>
    Task<bool> RemovePlatformFromGameAsync(Guid gameId, Guid platformId);

    /// <summary>
    /// Добавляет жанр в игру.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <returns>true, если жанр успешно добавлен; иначе false.</returns>
    Task<bool> AddGenreToGameAsync(Guid gameId, Guid genreId);

    /// <summary>
    /// Удаляет жанр из игры.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <returns>true, если жанр успешно удалён; иначе false.</returns>
    Task<bool> RemoveGenreFromGameAsync(Guid gameId, Guid genreId);

    /// <summary>
    /// Добавляет фичу в игру.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="featureId">Идентификатор фичи.</param>
    /// <returns>true, если фича успешно добавлена; иначе false.</returns>
    Task<bool> AddFeatureToGameAsync(Guid gameId, Guid featureId);

    /// <summary>
    /// Удаляет фичу из игры.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <param name="featureId">Идентификатор фичи.</param>
    /// <returns>true, если фича успешно удалена; иначе false.</returns>
    Task<bool> RemoveFeatureFromGameAsync(Guid gameId, Guid featureId);

    /// <summary>
    /// Удаляет игру по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>true, если игра успешно удалена; иначе false.</returns>
    Task<bool> DeleteGameAsync(Guid gameId);
}