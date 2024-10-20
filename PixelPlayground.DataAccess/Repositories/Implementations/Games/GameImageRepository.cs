using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class GameImageRepository : IGameImageRepository
{
    private readonly PixelBaseContext _context;

    public GameImageRepository(PixelBaseContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<GameImageEntity>> GetImagesByGameIdAsync(Guid gameId)
    {
        return await _context.GameImages.Where(gm => gm.GameId == gameId).ToListAsync();
    }

    public async Task<bool> UploadGameImageAsync(GameImageEntity gameImage)
    {
        var existingGameImage = await _context.GameImages.FirstOrDefaultAsync(gm => gm.ImageId == gameImage.ImageId);

        if (existingGameImage != null)
        {
            return false;
        }

        _context.GameImages.Add(gameImage);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UploadGameImageAsync(IEnumerable<GameImageEntity> gameImages)
    {
        var imageUrls = gameImages.Select(img => img.ImageUrl).ToList();
        var existingImages = await _context.GameImages
            .Where(gm => imageUrls.Contains(gm.ImageUrl))
            .Select(gm => gm.ImageUrl)
            .ToListAsync();

        var newImages = gameImages
            .Where(img => !existingImages.Contains(img.ImageUrl))
            .ToList();

        if (newImages.Count == 0) return false;

        _context.GameImages.AddRange(newImages);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteGameImageAsync(Guid imageId)
    {
        var existingGameImage = await _context.GameImages.FindAsync(imageId);
        if (existingGameImage == null) return false;

        _context.GameImages.Remove(existingGameImage);
        return await _context.SaveChangesAsync() > 0;
    }
}