using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;


/// <summary>
/// Интерфейс для работы с жанрами.
/// </summary>
public interface IGenreRepository
{
    /// <summary>
    /// Проверяет, существует ли жанр по его имени.
    /// </summary>
    /// <param name="genreName">Название жанра.</param>
    /// <returns>true, если жанр существует; иначе false.</returns>
    Task<bool> GenreExistsAsync(string genreName);

    /// <summary>
    /// Создает новый жанр.
    /// </summary>
    /// <param name="genreName">Название жанра, который нужно создать.</param>
    /// <returns>true, если жанр успешно создан; иначе false.</returns>
    Task<bool> CreateGenreAsync(string genreName);

    /// <summary>
    /// Получает все жанры.
    /// </summary>
    /// <returns>Список всех жанров.</returns>
    Task<IEnumerable<GenreEntity>> GetAllGenresAsync();

    /// <summary>
    /// Получает идентификатор жанра по его имени.
    /// </summary>
    /// <param name="genreName">Название жанра.</param>
    /// <returns>Идентификатор жанра, если жанр найден; иначе null.</returns>
    Task<Guid?> GetIdGenreByNameAsync(string genreName);

    /// <summary>
    /// Получает жанр по его идентификатору.
    /// </summary>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <returns>Объект жанра, если жанр найден; иначе null.</returns>
    Task<GenreEntity?> GetGenreByIdAsync(Guid genreId);

    /// <summary>
    /// Обновляет существующий жанр.
    /// </summary>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <param name="newGenreName">Новое название жанра.</param>
    /// <returns>true, если жанр успешно обновлён; иначе false.</returns>
    Task<bool> UpdateGenreAsync(Guid genreId, string newGenreName);

    /// <summary>
    /// Удаляет жанр по его идентификатору (мягкое удаление).
    /// </summary>
    /// <param name="genreId">Идентификатор жанра.</param>
    /// <returns>true, если жанр успешно удалён; иначе false.</returns>
    Task<bool> DeleteGenreAsync(Guid genreId);
}