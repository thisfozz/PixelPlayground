using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Entities.Reviews;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class GameRepository : IGameRepository
{
    private readonly PixelBaseContext _context;

    public GameRepository(PixelBaseContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateGameAsync(GameEntity game)
    {
        var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.Title == game.Title);
        if (existingGame != null)
        {
            return false;
        }

        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<GameEntity>> GetAllGamesAsync()
    {
        return await _context.Games
            .Include(game => game.Developer)
            .Include(game => game.Publisher)
            .Include(game => game.Platforms)
            .Include(game => game.GameImages)
            .Include(game => game.Reviews)
            .Include(game => game.SystemRequirements)
            .Include(game => game.Features)
            .Include(game => game.Genres)
            .ToListAsync();
    }

    public async Task<GameEntity?> GetGameByIdAsync(Guid gameId)
    {
        return await _context.Games
            .Include(game => game.Developer)
            .Include(game => game.Publisher)
            .Include(game => game.Platforms)
            .Include(game => game.GameImages)
            .Include(game => game.Reviews)
            .Include(game => game.SystemRequirements)
            .Include(game => game.Features)
            .Include(game => game.Genres)
            .FirstOrDefaultAsync(game => game.GameId == gameId);
    }

    public async Task<GameEntity?> GetGameByNameAsync(string name)
    {
        return await _context.Games
            .Include(game => game.Developer)
            .Include(game => game.Publisher)
            .Include(game => game.Platforms)
            .Include(game => game.GameImages)
            .Include(game => game.Reviews)
            .Include(game => game.SystemRequirements)
            .Include(game => game.Features)
            .Include(game => game.Genres)
            .FirstOrDefaultAsync(game => game.Title == name);
    }

    public async Task<string?> GetTitleByGameId(Guid gameId)
    {
        var existingGame = await _context.Games.FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.Title;
    }

    public async Task<DeveloperEntity?> GetDeveloperByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.Include(game => game.Developer).FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.Developer;
    }

    public async Task<PublisherEntity?> GetPublisherByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.Include(game => game.Publisher).FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.Publisher;
    }

    public async Task<IEnumerable<FeatureEntity?>> GetFeaturesByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.Include(game => game.Features).FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.Features ?? Enumerable.Empty<FeatureEntity>();
    }

    public async Task<IEnumerable<GameImageEntity?>> GetImagesByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.Include(game => game.GameImages).FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.GameImages ?? Enumerable.Empty<GameImageEntity>();
    }

    public async Task<IEnumerable<GenreEntity?>> GetGenresByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.Include(game => game.Genres).FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.Genres ?? Enumerable.Empty<GenreEntity>();
    }

    public async Task<IEnumerable<PlatformEntity?>> GetPlatformsByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.Include(game => game.Platforms).FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.Platforms ?? Enumerable.Empty<PlatformEntity>();
    }

    public async Task<IEnumerable<SystemRequirementEntity>> GetSystemRequirementsByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.Include(game => game.SystemRequirements).FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.SystemRequirements ?? Enumerable.Empty<SystemRequirementEntity>();
    }

    public async Task<IEnumerable<ReviewEntity?>> GetReviewsByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.Include(game => game.Reviews).FirstOrDefaultAsync(game => game.GameId == gameId);
        return existingGame?.Reviews ?? Enumerable.Empty<ReviewEntity>();
    }

    public async Task<bool> UpdateGameRatingAsync(Guid gameId, uint rating)
    {
        var existingGame = await _context.Games.FirstOrDefaultAsync(game => game.GameId == gameId);
        if (existingGame == null)
        {
            return false;
        }

        existingGame.Rating = rating;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateGameAsync(Guid gameId, GameEntity game)
    {
        var existingGame = await _context.Games
            .Include(g => g.GameImages)
            .Include(g => g.Features)
            .Include(g => g.SystemRequirements)
            .Include(g => g.Genres)
            .Include(g => g.Platforms)
            .FirstOrDefaultAsync(game => game.GameId == gameId);

        if (existingGame == null)
        {
            return false;
        }

        existingGame.Title = game.Title;
        existingGame.CoverUrl = game.CoverUrl;
        existingGame.ReleaseDate = game.ReleaseDate;
        existingGame.ReleaseDateStr = game.ReleaseDateStr;
        existingGame.Description = game.Description;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddPlatformToGameAsync(Guid gameId, PlatformEntity platform)
    {
        var existingGame = await _context.Games.Include(g => g.Platforms).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null || existingGame.Platforms.Any(p => p.PlatformId == platform.PlatformId))
        {
            return false;
        }

        existingGame.Platforms.Add(platform);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemovePlatformFromGameAsync(Guid gameId, Guid platformId)
    {
        var existingGame = await _context.Games.Include(g => g.Platforms).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null)
        {
            return false;
        }

        var platformToRemove = existingGame.Platforms.FirstOrDefault(p => p.PlatformId == platformId);
        if (platformToRemove != null)
        {
            existingGame.Platforms.Remove(platformToRemove);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> AddGenreToGameAsync(Guid gameId, GenreEntity genre)
    {
        var existingGame = await _context.Games.Include(g => g.Genres).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null || existingGame.Genres.Any(g => g.GenreId == genre.GenreId))
        {
            return false;
        }

        existingGame.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveGenreFromGameAsync(Guid gameId, Guid genreId)
    {
        var existingGame = await _context.Games.Include(g => g.Genres).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null)
        {
            return false;
        }

        var genreToRemove = existingGame.Genres.FirstOrDefault(g => g.GenreId == genreId);
        if (genreToRemove != null)
        {
            existingGame.Genres.Remove(genreToRemove);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> AddFeatureToGameAsync(Guid gameId, FeatureEntity feature)
    {
        var existingGame = await _context.Games.Include(g => g.Features).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null || existingGame.Features.Any(f => f.FeatureId == feature.FeatureId))
        {
            return false;
        }

        existingGame.Features.Add(feature);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveFeatureFromGameAsync(Guid gameId, Guid featureId)
    {
        var existingGame = await _context.Games.Include(g => g.Features).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null)
        {
            return false;
        }

        var featureToRemove = existingGame.Features.FirstOrDefault(f => f.FeatureId == featureId);
        if (featureToRemove != null)
        {
            existingGame.Features.Remove(featureToRemove);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> DeleteGameAsync(Guid gameId)
    {
        var existingGame = await _context.Games.FirstOrDefaultAsync(game => game.GameId == gameId);
        if (existingGame == null)
        {
            return false;
        }

        _context.Games.Remove(existingGame);
        await _context.SaveChangesAsync();

        return true;
    }
}