using DataAccess.Entities.Users;

namespace DataAccess.Repositories.Contracts.Interfaces.Users;

public interface IRoleRepository
{
    // Проверка, существует ли роль по её имени
    Task<bool> RoleExistsAsync(string roleName);

    // Создание новой роли
    Task<bool> CreateRoleAsync(string roleName);

    // Получение всех ролей
    Task<IEnumerable<RoleEntity>> GetAllRolesAsync();

    // Получение роли по её ID
    Task<RoleEntity?> GetRoleByIdAsync(int roleId);

    // Получение id роли по её имени
    Task<int> GetIdRoleByNameAsync(string roleName);

    // Обновление роли
    Task<bool> UpdateRoleAsync(int roleId, string newRoleName);

    // Удаление роли по её ID
    Task<bool> DeleteRoleAsync(int roleId);
}