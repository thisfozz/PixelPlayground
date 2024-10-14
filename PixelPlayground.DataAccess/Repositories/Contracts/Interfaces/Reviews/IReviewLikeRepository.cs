using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Reviews;


/// <summary>
/// Интерфейс для работы с лайками и дизлайками к отзывам.
/// </summary>
public interface IReviewLikeRepository
{
    /// <summary>
    /// Добавляет лайк или дизлайк к отзыву.
    /// </summary>
    /// <param name="reviewLike">Объект, представляющий лайк или дизлайк к отзыву.</param>
    /// <returns>true, если лайк или дизлайк успешно добавлен; иначе false.</returns>
    Task<bool> AddLikeAsync(ReviewLikeEntity reviewLike);

    /// <summary>
    /// Получает лайки и дизлайки для конкретного отзыва.
    /// </summary>
    /// <param name="reviewUserId">Идентификатор пользователя, который оставил отзыв.</param>
    /// <param name="reviewGameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <returns>Список лайков и дизлайков для данного отзыва.</returns>
    Task<IEnumerable<ReviewLikeEntity>> GetLikesByReviewAsync(Guid reviewUserId, Guid reviewGameId);

    /// <summary>
    /// Проверяет, поставил ли пользователь лайк или дизлайк к отзыву.
    /// </summary>
    /// <param name="reviewUserId">Идентификатор пользователя, который оставил отзыв.</param>
    /// <param name="reviewGameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <param name="userId">Идентификатор пользователя, статус лайка которого нужно проверить.</param>
    /// <returns>Объект ReviewLikeEntity, представляющий статус лайка, если он существует; иначе null.</returns>
    Task<ReviewLikeEntity?> GetUserLikeStatusAsync(Guid reviewUserId, Guid reviewGameId, Guid userId);

    /// <summary>
    /// Удаляет лайк или дизлайк к отзыву.
    /// </summary>
    /// <param name="reviewUserId">Идентификатор пользователя, который оставил отзыв.</param>
    /// <param name="reviewGameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <param name="userId">Идентификатор пользователя, чей лайк или дизлайк нужно удалить.</param>
    /// <returns>true, если лайк или дизлайк успешно удален; иначе false.</returns>
    Task<bool> RemoveLikeAsync(Guid reviewUserId, Guid reviewGameId, Guid userId);
}