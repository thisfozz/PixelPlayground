using DataAccess.Entities.Users;

namespace DataAccess.Repositories.Contracts.Interfaces.Users;

public interface IAddressRepository
{
    // Получить адрес по id пользователя
    Task<AddressEntity?> GetAddressByUserIdAsync(Guid userId);

    // Создать новый адрес для пользователя
    Task<bool> AddAddressAsync(AddressEntity address);

    // Обновить существующий адрес
    Task<bool> UpdateAddressAsync(AddressEntity newAddress);

    // Удалить адрес по идентификатору пользователя
    Task<bool> DeleteAddressByUserIdAsync(Guid userId);

    // Получить все адреса
    Task<IEnumerable<AddressEntity>> GetAllAddressesAsync();
}