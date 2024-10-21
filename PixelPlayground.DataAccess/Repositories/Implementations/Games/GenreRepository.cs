using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class GenreRepository : IGenreRepository
{
    private readonly PixelBaseContext _context;

    public GenreRepository(PixelBaseContext context)
    {
        _context = context;
    }

    public async Task<bool> GenreExistsAsync(string genreName)
    {
        return await _context.Genres.AnyAsync(genre => genre.Name == genreName);
    }

    public async Task<bool> CreateGenreAsync(string genreName)
    {
        if (await GenreExistsAsync(genreName)) return false;

        var newGenre = new GenreEntity
        {
            GenreId = Guid.NewGuid(),
            Name = genreName,
        };

        _context.Genres.Add(newGenre);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<GenreEntity>> GetAllGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Guid?> GetIdGenreByNameAsync(string genreName)
    {
        var existingGenre = await _context.Genres.FirstOrDefaultAsync(genre => genre.Name == genreName);

        return existingGenre?.GenreId;
    }

    public async Task<GenreEntity?> GetGenreByIdAsync(Guid genreId)
    {
        return await _context.Genres.FindAsync(genreId);
    }

    public async Task<bool> UpdateGenreAsync(Guid genreId, string newGenreName)
    {
        var existingGenre = await _context.Genres.FindAsync(genreId);
        if (existingGenre == null) return false;

        existingGenre.Name = newGenreName;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteGenreAsync(Guid genreId)
    {
        var existingGenre = await _context.Genres.FindAsync(genreId);
        if (existingGenre == null) return false;

        _context.Genres.Remove(existingGenre);
        return await _context.SaveChangesAsync() > 0;
    }
}