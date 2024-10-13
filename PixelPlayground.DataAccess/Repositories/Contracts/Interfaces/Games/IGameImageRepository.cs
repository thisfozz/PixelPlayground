using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface IGameImageRepository
{
    // Получение всех изображений для конкретной игры по ID игры
    Task<IEnumerable<GameImageEntity>> GetImagesByGameIdAsync(Guid gameId);

    // Загрузка одного изображения для игры
    Task<bool> UploadGameImageAsync(GameImageEntity gameImage);

    // Загрузка сразу изображений пачкой
    Task<bool> UploadGameImageAsync(IEnumerable<GameImageEntity> gameImages);

    // Удаление изображения по его ID
    Task<bool> DeleteGameImageAsync(Guid imageId);
}