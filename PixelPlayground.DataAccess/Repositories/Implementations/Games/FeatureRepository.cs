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

    private async Task<FeatureEntity?> FindFeatureByIdAsync(Guid featureId)
    {
        return await _context.Features.FirstOrDefaultAsync(feature => feature.FeatureId == featureId);
    }

    public Task<bool> FeatureExistsAsync(string featureName)
    {
        return _context.Features.AnyAsync(feature => feature.Name == featureName);
    }

    public async Task<bool> CreateFeatureAsync(string featureName)
    {
        if (await FeatureExistsAsync(featureName))
        {
            return false;
        }

        var newFeature = new FeatureEntity
        {
            FeatureId = Guid.NewGuid(),
            Name = featureName,
        };

        _context.Features.Add(newFeature);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<FeatureEntity>> GetAllFeaturesAsync()
    {
        return await _context.Features.ToListAsync();
    }

    public async Task<Guid?> GetIdDeveloperByNameAsync(string featureName)
    {
        var existingFeature = await _context.Features.FirstOrDefaultAsync(feature => feature.Name == featureName);

        return existingFeature?.FeatureId;
    }

    public async Task<FeatureEntity?> GetFeatureByIdAsync(Guid featureId)
    {
        return await FindFeatureByIdAsync(featureId);
    }

    public async Task<bool> UpdateFeatureAsync(Guid featureId, string newFeatureName)
    {
        var existingFeature = await FindFeatureByIdAsync(featureId);

        if (existingFeature == null)
        {
            return false;
        }

        existingFeature.Name = newFeatureName;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteFeatureAsync(Guid featureId)
    {
        var existingFeature = await FindFeatureByIdAsync(featureId);

        if (existingFeature == null)
        {
            return false;
        }

        _context.Features.Remove(existingFeature);
        await _context.SaveChangesAsync();

        return true;
    }
}