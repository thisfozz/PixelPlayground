using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Reviews;

public interface IReviewLikeRepository
{
    // Добавление лайка или дизлайка к отзыву
    Task<bool> AddLikeAsync(ReviewLikeEntity reviewLike);

    // Получение лайков/дизлайков для конкретного отзыва
    Task<IEnumerable<ReviewLikeEntity>> GetLikesByReviewAsync(Guid reviewUserId, Guid reviewGameId);

    // Проверка, поставил ли пользователь лайк или дизлайк к отзыву
    Task<ReviewLikeEntity?> GetUserLikeStatusAsync(Guid reviewUserId, Guid reviewGameId, Guid userId);

    // Удаление лайка или дизлайка
    Task<bool> RemoveLikeAsync(Guid reviewUserId, Guid reviewGameId, Guid userId);
}