using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Reviews;


/// <summary>
/// Интерфейс для работы с комментариями к отзывам.
/// </summary>
public interface IReviewCommentRepository
{
    /// <summary>
    /// Создает новый комментарий к отзыву на игру.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, к которой относится комментарий.</param>
    /// <param name="reviewId">Идентификатор отзыва, к которому добавляется комментарий.</param>
    /// <param name="comment">Объект комментария, который нужно создать, содержащий текст и информацию о пользователе.</param>
    /// <returns>true, если комментарий успешно создан; иначе false.</returns>
    Task<bool> AddCommentAsync(Guid gameId, Guid reviewId, ReviewCommentEntity comment);

    /// <summary>
    /// Получает все комментарии для конкретного отзыва на игру.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, к которой относится отзыв.</param>
    /// <param name="userId">Идентификатор пользователя, который оставил отзыв.</param>
    /// <param name="reviewId">Идентификатор отзыва, к которому оставлены комментарии.</param>
    /// <returns>Список комментариев к отзыву.</returns>
    Task<IEnumerable<ReviewCommentEntity>> GetCommentsByReviewAsync(Guid gameId, Guid userId, Guid reviewId);

    /// <summary>
    /// Удаляет комментарий к отзыву на игру.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, к которой относится комментарий.</param>
    /// <param name="reviewId">Идентификатор отзыва, к которому относится комментарий.</param>
    /// <param name="commentId">Идентификатор комментария, который нужно удалить.</param>
    /// <returns>true, если комментарий успешно удален; иначе false.</returns>
    Task<bool> DeleteCommentAsync(Guid gameId, Guid reviewId, Guid commentId);
}