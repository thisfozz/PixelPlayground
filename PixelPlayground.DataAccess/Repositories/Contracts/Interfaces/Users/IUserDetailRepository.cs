using DataAccess.Entities.Users;

namespace DataAccess.Repositories.Contracts.Interfaces.Users;


/// <summary>
/// Интерфейс для работы с деталями пользователя.
/// </summary>
public interface IUserDetailRepository
{
    /// <summary>
    /// Получает информацию о пользователе по его ID.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, информацию о котором нужно получить.</param>
    /// <returns>Объект UserDetailEntity, представляющий информацию о пользователе, если он существует; иначе null.</returns>
    Task<UserDetailEntity?> GetUserDetailByUserIdAsync(Guid userId);

    /// <summary>
    /// Обновляет информацию о пользователе.
    /// </summary>
    /// <param name="userDetail">Объект, содержащий обновленную информацию о пользователе.</param>
    /// <returns>true, если информация успешно обновлена; иначе false.</returns>
    Task<bool> UpdateUserDetailAsync(UserDetailEntity userDetail);

    /// <summary>
    /// Загружает аватарку пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, которому принадлежит аватарка.</param>
    /// <param name="avatarUrl">URL аватарки пользователя.</param>
    /// <returns>true, если аватарка успешно загружена; иначе false.</returns>
    Task<bool> UploadAvatarAsync(Guid userId, string avatarUrl);
}