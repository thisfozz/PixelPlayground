using DataAccess.Entities.Reviews;

namespace DataAccess.Repositories.Contracts.Interfaces.Reviews;


/// <summary>
/// Интерфейс для работы с отзывами пользователей к играм.
/// </summary>
public interface IReviewRepository
{
    /// <summary>
    /// Создает новый отзыв.
    /// </summary>
    /// <param name="review">Объект, представляющий новый отзыв.</param>
    /// <returns>true, если отзыв успешно создан; иначе false.</returns>
    Task<bool> CreateReviewAsync(ReviewEntity review);

    /// <summary>
    /// Получает все отзывы для конкретной игры.
    /// </summary>
    /// <param name="gameId">Идентификатор игры, для которой нужно получить отзывы.</param>
    /// <returns>Список отзывов для указанной игры.</returns>
    Task<IEnumerable<ReviewEntity>> GetReviewsByGameIdAsync(Guid gameId);

    /// <summary>
    /// Получает отзыв пользователя для конкретной игры.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чье мнение нужно получить.</param>
    /// <param name="gameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <returns>Объект ReviewEntity, представляющий отзыв, если он существует; иначе null.</returns>
    Task<ReviewEntity?> GetReviewByUserAndGameIdAsync(Guid userId, Guid gameId);

    /// <summary>
    /// Обновляет существующий отзыв.
    /// </summary>
    /// <param name="review">Объект, представляющий обновленный отзыв.</param>
    /// <returns>true, если отзыв успешно обновлен; иначе false.</returns>
    Task<bool> UpdateReviewAsync(ReviewEntity review);

    /// <summary>
    /// Удаляет отзыв.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, который оставил отзыв.</param>
    /// <param name="gameId">Идентификатор игры, к которой оставлен отзыв.</param>
    /// <returns>true, если отзыв успешно удален; иначе false.</returns>
    Task<bool> DeleteReviewAsync(Guid userId, Guid gameId);
}