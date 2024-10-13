using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface IPublisherRepository
{
    // Проверка, существует ли издатель по его имени
    Task<bool> PublisherExistsAsync(string publisherName);

    // Создание нового издателя
    Task<bool> CreatePublisherAsync(string publisherName);

    // Получение всех издателей
    Task<IEnumerable<PublisherEntity>> GetAllPublishersAsync();

    // Получение ID издателя по его имени
    Task<Guid?> GetIdPublisherByNameAsync(string publisherName);

    // Получение издателя по его ID
    Task<PublisherEntity?> GetPublisherByIdAsync(Guid publisherId);

    // Обновление издателя
    Task<bool> UpdatePublisherAsync(Guid publisherId, string newPublisherName);

    // Удаление издателя по его ID (мягкое удаление)
    Task<bool> DeletePublisherAsync(Guid publisherId);
}