using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Reviews;


/// <summary>
/// Интерфейс для работы с отзывами к играм.
/// </summary>
public interface IReviewRepository
{
    /// <summary>
    /// Создает новый отзыв с указанным статусом лайка или дизлайка.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <param name="review">Объект, представляющий новый отзыв.</param>
    /// <param name="isLike">Указывает, является ли отзыв лайком (true) или дизлайком (false).</param>
    /// <returns>true, если отзыв успешно создан; иначе false.</returns>
    Task<bool> AddReviewWithLikeAsync(Guid gameId, ReviewEntity review, bool isLike);

    /// <summary>
    /// Получает все отзывы для конкретной игры.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, для которой нужно получить отзывы.</param>
    /// <returns>Список отзывов для указанной игры.</returns>
    Task<IEnumerable<ReviewEntity>> GetReviewsByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает отзыв пользователя для конкретной игры.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <param name="userId">Идентификатор пользователя, чье мнение нужно получить.</param>
    /// <returns>Объект ReviewEntity, представляющий отзыв, если он существует; иначе null.</returns>
    Task<ReviewEntity?> GetReviewByUserAndGameIdAsync(Guid gameId, Guid userId);

    /// <summary>
    /// Обновляет существующий отзыв и его статус лайка или дизлайка.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <param name="review">Объект, представляющий обновленный отзыв.</param>
    /// <param name="isLike">Указывает, является ли отзыв лайком (true) или дизлайком (false).</param>
    /// <returns>true, если отзыв успешно обновлен; иначе false.</returns>
    Task<bool> UpdateReviewAsync(Guid gameId, ReviewEntity review, bool isLike);

    /// <summary>
    /// Удаляет отзыв.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <param name="userId">Идентификатор пользователя, который оставил отзыв.</param>
    /// <returns>true, если отзыв успешно удален; иначе false.</returns>
    Task<bool> DeleteReviewAsync(Guid gameId, Guid userId);
}