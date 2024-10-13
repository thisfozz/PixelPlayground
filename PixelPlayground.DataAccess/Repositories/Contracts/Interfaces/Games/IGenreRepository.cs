using DataAccess.Entities.Games;

namespace DataAccess.Repositories.Contracts.Interfaces.Games;

public interface IGenreRepository
{
    // Проверка, существует ли жанр по его имени
    Task<bool> GenreExistsAsync(string genreName);

    // Создание нового жанра
    Task<bool> CreateGenreAsync(string genreName);

    // Получение всех жанров
    Task<IEnumerable<GenreEntity>> GetAllGenresAsync();

    // Получение ID жанра по его имени
    Task<Guid?> GetIdGenreByNameAsync(string genreName);

    // Получение жанра по его ID
    Task<GenreEntity?> GetGenreByIdAsync(Guid genreId);

    // Обновление жанра
    Task<bool> UpdateGenreAsync(Guid genreId, string newGenreName);

    // Удаление жанра по его ID (мягкое удаление)
    Task<bool> DeleteGenreAsync(Guid genreId);
}