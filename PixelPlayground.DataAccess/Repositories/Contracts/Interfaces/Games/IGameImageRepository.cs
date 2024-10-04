using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface IGameImageRepository
{
    // Загрузка одного изображения для игры
    Task<bool> UpdateGameImageAsync(GameImageEntity gameImage);

    // Загрузка сразу изображений пачкой
    Task<bool> UpdateGameImageAsync(IEnumerable<GameImageEntity> gameImages);

    // Получение всех изображений для конкретной игры по ID игры
    Task<IEnumerable<GameImageEntity>> GetImagesByGameIdAsync(Guid gameId);

    // Удаление изображения по его ID
    Task<bool> DeleteGameImageAsync(Guid imageId);
}