using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Reviews;

public interface IReviewRepository
{
    // Создание нового отзыва
    Task<bool> CreateReviewAsync(ReviewEntity review);

    // Получение всех отзывов для конкретной игры
    Task<IEnumerable<ReviewEntity>> GetReviewsByGameIdAsync(Guid gameId);

    // Получение отзыва пользователя для конкретной игры
    Task<ReviewEntity?> GetReviewByUserAndGameIdAsync(Guid userId, Guid gameId);

    // Обновление отзыва
    Task<bool> UpdateReviewAsync(ReviewEntity review);

    // Удаление отзыва
    Task<bool> DeleteReviewAsync(Guid userId, Guid gameId);
}