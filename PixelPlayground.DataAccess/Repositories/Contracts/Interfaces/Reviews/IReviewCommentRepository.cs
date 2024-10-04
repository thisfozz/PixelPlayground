using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Reviews;

public interface IReviewCommentRepository
{
    // Создание нового комментария к отзыву
    Task<bool> CreateCommentAsync(ReviewCommentEntity comment);

    // Получение всех комментариев для конкретного отзыва
    Task<IEnumerable<ReviewCommentEntity>> GetCommentsByReviewAsync(Guid reviewUserId, Guid reviewGameId);

    // Удаление комментария
    Task<bool> DeleteCommentAsync(Guid commentId);
}