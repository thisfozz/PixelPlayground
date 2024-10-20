﻿using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Entities.Reviews;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class GameRepository : IGameRepository
{
    private readonly PixelBaseContext _context;
    private readonly IPlatformRepository _platformRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IFeatureRepository _featureRepository;

    public GameRepository(PixelBaseContext context, IPlatformRepository platformRepository, IGenreRepository genreRepository, IFeatureRepository featureRepository)
    {
        _context = context;
        _platformRepository = platformRepository;
        _genreRepository = genreRepository;
        _featureRepository = featureRepository;
    }

    public async Task<bool> CreateGameAsync(GameEntity game)
    {
        var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.Title == game.Title);
        if (existingGame != null)
        {
            return false;
        }

        _context.Games.Add(game);
        return await _context.SaveChangesAsync() > 0;
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
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return false;

        existingGame.Rating = rating;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateGameAsync(Guid gameId, GameEntity game)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return false;

        existingGame.Title = game.Title;
        existingGame.CoverUrl = game.CoverUrl;
        existingGame.ReleaseDate = game.ReleaseDate;
        existingGame.ReleaseDateStr = game.ReleaseDateStr;
        existingGame.Description = game.Description;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddPlatformToGameAsync(Guid gameId, Guid platformId)
    {
        var existingGame = await _context.Games.Include(g => g.Platforms).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null || existingGame.Platforms.Any(p => p.PlatformId == platformId)) return false;

        var platform = await _platformRepository.GetPlatformByIdAsync(platformId);
        if (platform != null)
        {
            existingGame.Platforms.Add(platform);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> RemovePlatformFromGameAsync(Guid gameId, Guid platformId)
    {
        var existingGame = await _context.Games.Include(g => g.Platforms).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null) return false;

        var platformToRemove = existingGame.Platforms.FirstOrDefault(p => p.PlatformId == platformId);
        if (platformToRemove != null)
        {
            existingGame.Platforms.Remove(platformToRemove);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> AddGenreToGameAsync(Guid gameId, Guid genreId)
    {
        var existingGame = await _context.Games.Include(g => g.Genres).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null || existingGame.Genres.Any(g => g.GenreId == genreId)) return false;

        var genre = await _genreRepository.GetGenreByIdAsync(genreId);
        if (genre != null)
        {
            existingGame.Genres.Add(genre);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> RemoveGenreFromGameAsync(Guid gameId, Guid genreId)
    {
        var existingGame = await _context.Games.Include(g => g.Genres).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null) return false;

        var genreToRemove = await _genreRepository.GetGenreByIdAsync(genreId);
        if (genreToRemove != null)
        {
            existingGame.Genres.Remove(genreToRemove);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> AddFeatureToGameAsync(Guid gameId, Guid featureId)
    {
        var existingGame = await _context.Games.Include(g => g.Features).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null || existingGame.Features.Any(f => f.FeatureId == featureId)) return false;

        var feature = await _featureRepository.GetFeatureByIdAsync(featureId);
        if(feature != null)
        {
            existingGame.Features.Add(feature);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> RemoveFeatureFromGameAsync(Guid gameId, Guid featureId)
    {
        var existingGame = await _context.Games.Include(g => g.Features).FirstOrDefaultAsync(g => g.GameId == gameId);
        if (existingGame == null) return false;

        var featureToRemove = await _featureRepository.GetFeatureByIdAsync(featureId);
        if (featureToRemove != null)
        {
            existingGame.Features.Remove(featureToRemove);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> DeleteGameAsync(Guid gameId)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return false;

        _context.Games.Remove(existingGame);
        return await _context.SaveChangesAsync() > 0;
    }
}