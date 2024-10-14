using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Reviews;


/// <summary>
/// Интерфейс для работы с комментариями к отзывам.
/// </summary>
public interface IReviewCommentRepository
{
    /// <summary>
    /// Создает новый комментарий к отзыву.
    /// </summary>
    /// <param name="comment">Объект комментария, который нужно создать.</param>
    /// <returns>true, если комментарий успешно создан; иначе false.</returns>
    Task<bool> CreateCommentAsync(ReviewCommentEntity comment);

    /// <summary>
    /// Получает все комментарии для конкретного отзыва.
    /// </summary>
    /// <param name="reviewUserId">Идентификатор пользователя, который оставил отзыв.</param>
    /// <param name="reviewGameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <returns>Список комментариев к отзыву.</returns>
    Task<IEnumerable<ReviewCommentEntity>> GetCommentsByReviewAsync(Guid reviewUserId, Guid reviewGameId);

    /// <summary>
    /// Удаляет комментарий.
    /// </summary>
    /// <param name="commentId">Идентификатор комментария, который нужно удалить.</param>
    /// <returns>true, если комментарий успешно удален; иначе false.</returns>
    Task<bool> DeleteCommentAsync(Guid commentId);
}