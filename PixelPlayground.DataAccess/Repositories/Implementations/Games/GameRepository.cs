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

    public async Task<bool> UpdateGameAsync(Guid gameId, GameEntity game, List<Guid> genresToRemove, List<Guid> platformsToRemove, List<Guid> featuresToRemove)
    {
        var existingGame = await _context.Games
            .Include(g => g.GameImages)
            .Include(g => g.Features)
            .Include(g => g.SystemRequirements)
            .Include(g => g.Genres)
            .Include(g => g.Platforms)
            .FirstOrDefaultAsync(g => g.GameId == gameId);

        if (existingGame == null)
        {
            return false;
        }

        existingGame.Title = game.Title;
        existingGame.CoverUrl = game.CoverUrl;
        existingGame.ReleaseDate = game.ReleaseDate;
        existingGame.Description = game.Description;

        UpdatePlatforms(existingGame, game.Platforms);
        UpdateGenres(existingGame, game.Genres);
        UpdateFeatures(existingGame, game.Features);
        UpdateSystemRequirements(existingGame, game.SystemRequirements);
        UpdateImages(existingGame, game.GameImages);

        await _context.SaveChangesAsync();

        return true;
    }

    private void UpdatePlatforms(GameEntity existingGame, ICollection<PlatformEntity> newPlatforms)
    {
        foreach (var platform in newPlatforms)
        {
            if (!existingGame.Platforms.Any(p => p.PlatformId == platform.PlatformId))
            {
                existingGame.Platforms.Add(platform);
            }
        }

        var platformIdsToRemove = existingGame.Platforms.Select(p => p.PlatformId).Except(newPlatforms.Select(p => p.PlatformId)).ToList();
        foreach (var platformId in platformIdsToRemove)
        {
            var platformToRemove = existingGame.Platforms.FirstOrDefault(p => p.PlatformId == platformId);
            if (platformToRemove != null)
            {
                existingGame.Platforms.Remove(platformToRemove);
            }
        }
    }

    private void UpdateGenres(GameEntity existingGame, ICollection<GenreEntity> newGenres)
    {
        foreach (var genre in newGenres)
        {
            if (!existingGame.Genres.Any(g => g.GenreId == genre.GenreId))
            {
                existingGame.Genres.Add(genre);
            }
        }

        var genreIdsToRemove = existingGame.Genres.Select(g => g.GenreId).Except(newGenres.Select(g => g.GenreId)).ToList();
        foreach (var genreId in genreIdsToRemove)
        {
            var genreToRemove = existingGame.Genres.FirstOrDefault(g => g.GenreId == genreId);
            if (genreToRemove != null)
            {
                existingGame.Genres.Remove(genreToRemove);
            }
        }
    }

    private void UpdateFeatures(GameEntity existingGame, ICollection<FeatureEntity> newFeatures)
    {
        foreach (var feature in newFeatures)
        {
            if (!existingGame.Features.Any(f => f.FeatureId == feature.FeatureId))
            {
                existingGame.Features.Add(feature);
            }
        }

        var featureIdsToRemove = existingGame.Features.Select(f => f.FeatureId).Except(newFeatures.Select(f => f.FeatureId)).ToList();
        foreach (var featureId in featureIdsToRemove)
        {
            var featureToRemove = existingGame.Features.FirstOrDefault(f => f.FeatureId == featureId);
            if (featureToRemove != null)
            {
                existingGame.Features.Remove(featureToRemove);
            }
        }
    }

    private void UpdateSystemRequirements(GameEntity existingGame, ICollection<SystemRequirementEntity> newRequirements)
    {
        foreach (var requirement in newRequirements)
        {
            if (!existingGame.SystemRequirements.Any(sr => sr.RequirementId == requirement.RequirementId))
            {
                existingGame.SystemRequirements.Add(requirement);
            }
        }

        var requirementIdsToRemove = existingGame.SystemRequirements.Select(sr => sr.RequirementId).Except(newRequirements.Select(sr => sr.RequirementId)).ToList();
        foreach (var requirementId in requirementIdsToRemove)
        {
            var requirementToRemove = existingGame.SystemRequirements.FirstOrDefault(sr => sr.RequirementId == requirementId);
            if (requirementToRemove != null)
            {
                existingGame.SystemRequirements.Remove(requirementToRemove);
            }
        }
    }

    private void UpdateImages(GameEntity existingGame, ICollection<GameImageEntity> newImages)
    {
        foreach (var image in newImages)
        {
            if (!existingGame.GameImages.Any(i => i.ImageUrl == image.ImageUrl))
            {
                existingGame.GameImages.Add(image);
            }
        }

        var imageUrlsToRemove = existingGame.GameImages.Select(i => i.ImageUrl).Except(newImages.Select(i => i.ImageUrl)).ToList();
        foreach (var imageUrl in imageUrlsToRemove)
        {
            var imageToRemove = existingGame.GameImages.FirstOrDefault(i => i.ImageUrl == imageUrl);
            if (imageToRemove != null)
            {
                existingGame.GameImages.Remove(imageToRemove);
            }
        }
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