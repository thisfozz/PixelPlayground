using DataAccess.Entities.Users;

namespace DataAccess.Repositories.Contracts.Interfaces.Users;


/// <summary>
/// Интерфейс для работы с ролями пользователей.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Проверяет, существует ли роль по её имени.
    /// </summary>
    /// <param name="roleName">Имя роли, которую нужно проверить.</param>
    /// <returns>true, если роль существует; иначе false.</returns>
    Task<bool> RoleExistsAsync(string roleName);

    /// <summary>
    /// Создает новую роль.
    /// </summary>
    /// <param name="roleName">Имя новой роли.</param>
    /// <returns>true, если роль успешно создана; иначе false.</returns>
    Task<bool> CreateRoleAsync(string roleName);

    /// <summary>
    /// Получает все роли.
    /// </summary>
    /// <returns>Список всех ролей.</returns>
    Task<IEnumerable<RoleEntity>> GetAllRolesAsync();

    /// <summary>
    /// Получает роль по её ID.
    /// </summary>
    /// <param name="roleId">Идентификатор роли, которую нужно получить.</param>
    /// <returns>Объект RoleEntity, представляющий роль, если она существует; иначе null.</returns>
    Task<RoleEntity?> GetRoleByIdAsync(Guid roleId);

    /// <summary>
    /// Получает ID роли по её имени.
    /// </summary>
    /// <param name="roleName">Имя роли, ID которой нужно получить.</param>
    /// <returns>ID роли.</returns>
    Task<Guid?> GetIdRoleByNameAsync(string roleName);

    /// <summary>
    /// Обновляет существующую роль.
    /// </summary>
    /// <param name="roleId">Идентификатор роли, которую нужно обновить.</param>
    /// <param name="newRoleName">Новое имя для роли.</param>
    /// <returns>true, если роль успешно обновлена; иначе false.</returns>
    Task<bool> UpdateRoleAsync(Guid roleId, string newRoleName);

    /// <summary>
    /// Удаляет роль по её ID.
    /// </summary>
    /// <param name="roleId">Идентификатор роли, которую нужно удалить.</param>
    /// <returns>true, если роль успешно удалена; иначе false.</returns>
    Task<bool> DeleteRoleAsync(Guid roleId);
}