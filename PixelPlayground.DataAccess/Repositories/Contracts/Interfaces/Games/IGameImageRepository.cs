using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;


/// <summary>
/// Интерфейс для работы с изображениями игр.
/// </summary>
public interface IGameImageRepository
{
    /// <summary>
    /// Получает все изображения для конкретной игры по её идентификатору.
    /// </summary>
    /// <param name="gameId">Идентификатор игры.</param>
    /// <returns>Список изображений для указанной игры.</returns>
    Task<IEnumerable<GameImageEntity>> GetImagesByGameIdAsync(Guid gameId);

    /// <summary>
    /// Загружает одно изображение для игры.
    /// </summary>
    /// <param name="gameImage">Объект изображения игры, который нужно загрузить.</param>
    /// <returns>true, если изображение успешно загружено; иначе false.</returns>
    Task<bool> UploadGameImageAsync(GameImageEntity gameImage);

    /// <summary>
    /// Загружает сразу несколько изображений пачкой для игры.
    /// </summary>
    /// <param name="gameImages">Список объектов изображений игр, которые нужно загрузить.</param>
    /// <returns>true, если изображения успешно загружены; иначе false.</returns>
    Task<bool> UploadGameImageAsync(IEnumerable<GameImageEntity> gameImages);

    /// <summary>
    /// Удаляет изображение по его идентификатору.
    /// </summary>
    /// <param name="imageId">Идентификатор изображения, которое нужно удалить.</param>
    /// <returns>true, если изображение успешно удалено; иначе false.</returns>
    Task<bool> DeleteGameImageAsync(Guid imageId);
}