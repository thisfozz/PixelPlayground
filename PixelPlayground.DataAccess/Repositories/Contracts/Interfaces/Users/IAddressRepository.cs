using DataAccess.Entities.Users;

namespace DataAccess.Repositories.Contracts.Interfaces.Users;


/// <summary>
/// Интерфейс для работы с адресами пользователя.
/// </summary>
public interface IAddressRepository
{
    /// <summary>
    /// Получает адреса по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Список адресов, принадлежащих пользователю.</returns>
    Task<IEnumerable<AddressEntity>> GetAddressesByUserIdAsync(Guid userId);

    /// <summary>
    /// Получает адрес по его идентификатору.
    /// </summary>
    /// <param name="addressId">Идентификатор адреса.</param>
    /// <returns>Объект адреса или null, если адрес не найден.</returns>
    Task<AddressEntity?> GetAddressByIdAsync(Guid addressId);

    /// <summary>
    /// Создает новый адрес для пользователя.
    /// </summary>
    /// <param name="address">Объект адреса, который нужно добавить.</param>
    /// <returns>true, если адрес успешно добавлен; иначе false.</returns>
    Task<bool> AddAddressAsync(AddressEntity address);

    /// <summary>
    /// Обновляет существующий адрес.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, которому принадлежит адрес.</param>
    /// <param name="newAddress">Объект адреса с обновленными данными.</param>
    /// <returns>true, если адрес успешно обновлен; иначе false.</returns>
    Task<bool> UpdateAddressAsync(Guid userId, AddressEntity newAddress);

    /// <summary>
    /// Удаляет адрес по его идентификатору.
    /// </summary>
    /// <param name="addressId">Идентификатор адреса, который нужно удалить.</param>
    /// <returns>true, если адрес успешно удален; иначе false.</returns>
    Task<bool> DeleteAddressByIdAsync(Guid addressId);

    /// <summary>
    /// Удаляет все адреса, принадлежащие пользователю.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чьи адреса нужно удалить.</param>
    /// <returns>true, если адреса успешно удалены; иначе false.</returns>
    Task<bool> DeleteAllAddressesByUserIdAsync(Guid userId);
}