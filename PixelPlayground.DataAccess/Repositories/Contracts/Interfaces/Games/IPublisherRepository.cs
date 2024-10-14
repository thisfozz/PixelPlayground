using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;


/// <summary>
/// Интерфейс для работы с издателями.
/// </summary>
public interface IPublisherRepository
{
    /// <summary>
    /// Проверяет, существует ли издатель по его имени.
    /// </summary>
    /// <param name="publisherName">Название издателя.</param>
    /// <returns>true, если издатель существует; иначе false.</returns>
    Task<bool> PublisherExistsAsync(string publisherName);

    /// <summary>
    /// Создает нового издателя.
    /// </summary>
    /// <param name="publisherName">Название издателя, которого нужно создать.</param>
    /// <returns>true, если издатель успешно создан; иначе false.</returns>
    Task<bool> CreatePublisherAsync(string publisherName);

    /// <summary>
    /// Получает всех издателей.
    /// </summary>
    /// <returns>Список всех издателей.</returns>
    Task<IEnumerable<PublisherEntity>> GetAllPublishersAsync();

    /// <summary>
    /// Получает идентификатор издателя по его имени.
    /// </summary>
    /// <param name="publisherName">Название издателя.</param>
    /// <returns>Идентификатор издателя, если издатель найден; иначе null.</returns>
    Task<Guid?> GetIdPublisherByNameAsync(string publisherName);

    /// <summary>
    /// Получает издателя по его идентификатору.
    /// </summary>
    /// <param name="publisherId">Идентификатор издателя.</param>
    /// <returns>Объект издателя, если издатель найден; иначе null.</returns>
    Task<PublisherEntity?> GetPublisherByIdAsync(Guid publisherId);

    /// <summary>
    /// Обновляет существующего издателя.
    /// </summary>
    /// <param name="publisherId">Идентификатор издателя.</param>
    /// <param name="newPublisherName">Новое название издателя.</param>
    /// <returns>true, если издатель успешно обновлен; иначе false.</returns>
    Task<bool> UpdatePublisherAsync(Guid publisherId, string newPublisherName);

    /// <summary>
    /// Удаляет издателя по его идентификатору (мягкое удаление).
    /// </summary>
    /// <param name="publisherId">Идентификатор издателя.</param>
    /// <returns>true, если издатель успешно удален; иначе false.</returns>
    Task<bool> DeletePublisherAsync(Guid publisherId);
}