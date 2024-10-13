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
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UploadGameImageAsync(IEnumerable<GameImageEntity> gameImages)
    {
        foreach (var gameImage in gameImages)
        {
            var existingImage = await _context.GameImages
                .FirstOrDefaultAsync(gm => gm.ImageUrl == gameImage.ImageUrl);

            if (existingImage != null)
            {
                continue;
            }

            _context.GameImages.Add(gameImage);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteGameImageAsync(Guid imageId)
    {
        var existingGameImage = await _context.GameImages.FirstOrDefaultAsync(gm => gm.ImageId == imageId);

        if (existingGameImage == null)
        {
            return false;
        }

        _context.GameImages.Remove(existingGameImage);
        await _context.SaveChangesAsync();

        return true;
    }
}