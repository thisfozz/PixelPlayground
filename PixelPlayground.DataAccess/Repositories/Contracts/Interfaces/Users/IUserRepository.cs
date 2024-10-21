using DataAccess.Entities.Users;

namespace DataAccess.Repositories.Contracts.Interfaces.Users;


/// <summary>
/// Интерфейс для работы с пользователями.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Проверяет существование пользователя по логину.
    /// </summary>
    /// <param name="login">Логин пользователя, которого нужно проверить.</param>
    /// <returns>true, если пользователь с данным логином существует; иначе false.</returns>
    Task<bool> UserExistsByUsernameAsync(string login);

    /// <summary>
    /// Проверяет существование пользователя по email.
    /// </summary>
    /// <param name="email">Email пользователя, который нужно проверить.</param>
    /// <returns>true, если пользователь с данным email существует; иначе false.</returns>
    Task<bool> UserExistsByEmailAsync(string email);

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="user">Объект пользователя, который нужно создать.</param>
    /// <returns>true, если пользователь успешно создан; иначе false.</returns>
    Task<bool> RegisterUserAsync(UserEntity user);

    /// <summary>
    /// Получает информацию о пользователе по его ID.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, информацию о котором нужно получить.</param>
    /// <returns>Объект UserEntity, представляющий пользователя, если он существует; иначе null.</returns>
    Task<UserEntity?> GetUserByIdAsync(Guid userId);

    /// <summary>
    /// Получает идентификатор пользователя по его логину.
    /// </summary>
    /// <param name="login">Логин пользователя, идентификатор которого нужно получить.</param>
    /// <returns>Идентификатор пользователя, если он существует; иначе null.</returns>
    Task<Guid?> GetUserIdByLoginAsync(string login);

    /// <summary>
    /// Получает пользователя по логину.
    /// </summary>
    /// <param name="login">Логин пользователя, которого нужно найти.</param>
    /// <returns>Объект UserEntity, представляющий пользователя, если он существует; иначе null.</returns>
    Task<UserEntity?> GetUserByLoginAsync(string login);

    /// <summary>
    /// Получает пользователя по электронному адресу.
    /// </summary>
    /// <param name="email">Электронный адрес пользователя, которого нужно найти.</param>
    /// <returns>Объект UserEntity, представляющий пользователя, если он существует; иначе null.</returns>
    Task<UserEntity?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Обновляет имя пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чье имя нужно обновить.</param>
    /// <param name="displayName">Новое имя пользователя.</param>
    /// <returns>true, если имя успешно обновлено; иначе false.</returns>
    Task<bool> UpdateDisplayNameAsync(Guid userId, string displayName);

    /// <summary>
    /// Обновляет пароль пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чей пароль нужно обновить.</param>
    /// <param name="newPasswordHash">Новый хэш пароля.</param>
    /// <returns>true, если пароль успешно обновлен; иначе false.</returns>
    Task<bool> UpdatePasswordAsync(Guid userId, string newPasswordHash);

    /// <summary>
    /// Обновляет email пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чей email нужно обновить.</param>
    /// <param name="email">Новый email.</param>
    /// <returns>true, если email успешно обновлен; иначе false.</returns>
    Task<bool> UpdateEmailAsync(Guid userId, string email);

    /// <summary>
    /// Обновляет роль пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чью роль нужно обновить.</param>
    /// <param name="roleId">Идентификатор новой роли.</param>
    /// <returns>true, если роль успешно обновлена; иначе false.</returns>
    Task<bool> UpdateUserRoleAsync(Guid userId, Guid roleId);

    /// <summary>
    /// Пополняет баланс пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чей баланс нужно пополнить.</param>
    /// <param name="amount">Сумма, на которую нужно пополнить баланс.</param>
    /// <returns>true, если баланс успешно пополнен; иначе false.</returns>
    Task<bool> AddBalanceAsync(Guid userId, decimal amount);

    /// <summary>
    /// Уменьшает баланс пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чей баланс нужно уменьшить.</param>
    /// <param name="amount">Сумма, на которую нужно уменьшить баланс.</param>
    /// <returns>true, если баланс успешно пополнен; иначе false.</returns>
    Task<bool> DeductBalanceAsync(Guid userId, decimal amount);

    /// <summary>
    /// Удаляет (мягко) аккаунт пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, аккаунт которого нужно удалить.</param>
    /// <returns>true, если аккаунт успешно удален; иначе false.</returns>
    Task<bool> DeleteUserAsync(Guid userId);
}