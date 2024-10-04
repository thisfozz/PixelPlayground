using DataAccess.Entities.Users;

namespace DataAccess.Repositories.Contracts.Interfaces.Users;

public interface IUserDetailRepository
{
    // Обновить информацию о пользователе
    Task<bool> UpdateUserDetailAsync(UserDetailEntity userDetail);

    // Загрузить аватарку пользователя
    Task<bool> UploadAvatarAsync(string avatarUrl);

    // Получить информацию о пользователе по его ID
    Task<UserDetailEntity?> GetUserDetailByUserIdAsync(Guid userId);
}