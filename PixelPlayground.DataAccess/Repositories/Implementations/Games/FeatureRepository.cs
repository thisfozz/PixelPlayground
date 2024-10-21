using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class FeatureRepository : IFeatureRepository
{
    private readonly PixelBaseContext _context;

    public FeatureRepository(PixelBaseContext context)
    {
        _context = context;
    }

    public Task<bool> FeatureExistsAsync(string featureName)
    {
        return _context.Features.AnyAsync(feature => feature.Name == featureName);
    }

    public async Task<bool> AddFeatureAsync(string featureName)
    {
        if (await FeatureExistsAsync(featureName)) return false;

        var newFeature = new FeatureEntity
        {
            FeatureId = Guid.NewGuid(),
            Name = featureName,
        };

        _context.Features.Add(newFeature);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<FeatureEntity>> GetAllFeaturesAsync()
    {
        return await _context.Features.ToListAsync();
    }

    public async Task<Guid?> GetIdFeatureByNameAsync(string featureName)
    {
        var existingFeature = await _context.Features.FirstOrDefaultAsync(feature => feature.Name == featureName);

        return existingFeature?.FeatureId;
    }

    public async Task<FeatureEntity?> GetFeatureByIdAsync(Guid featureId)
    {
        return await _context.Features.FindAsync(featureId);
    }

    public async Task<bool> UpdateFeatureAsync(Guid featureId, string newFeatureName)
    {
        var existingFeature = await _context.Features.FindAsync(featureId);
        if (existingFeature == null) return false;

        existingFeature.Name = newFeatureName;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteFeatureAsync(Guid featureId)
    {
        var existingFeature = await _context.Features.FindAsync(featureId);
        if (existingFeature == null) return false;

        _context.Features.Remove(existingFeature);
        return await _context.SaveChangesAsync() > 0;
    }
}