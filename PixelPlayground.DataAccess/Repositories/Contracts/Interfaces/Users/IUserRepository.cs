using DataAccess.Entities.Users;

namespace DataAccess.Repositories.Contracts.Interfaces.Users;

public interface IUserRepository
{
    // Создать нового пользователя
    Task<bool> CreateUserAsync(UserEntity user);

    // Получить информацию о пользователе по его ID
    Task<UserEntity?> GetUserByIdAsync(Guid userId);

    // Получить пользователя по логину
    Task<UserEntity?> GetUserByLoginAsync(string login);

    // Обновить имя пользователя
    Task<bool> UpdateDisplayNameAsync(Guid userId, string displayName);

    // Обновить пароль пользователя
    Task<bool> UpdatePasswordAsync(Guid userId, string newPasswordHash);

    // Обновить email пользователя
    Task<bool> UpdateEmailAsync(Guid userId, string email);

    // Пополнить баланс пользователя
    Task<bool> AddBalanceAsync(Guid userId, decimal amount);

    // Удалить (мягко) аккаунт пользователя
    Task<bool> DeleteUserAsync(Guid userId);
}