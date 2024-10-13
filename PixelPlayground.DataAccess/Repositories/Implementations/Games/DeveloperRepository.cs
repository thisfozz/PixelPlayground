using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class DeveloperRepository : IDeveloperRepository
{
    private readonly PixelBaseContext _context;

    public DeveloperRepository(PixelBaseContext context)
    {
        _context = context;
    }

    private async Task<DeveloperEntity?> FindDeveloperByIdAsync(Guid developerId)
    {
        return await _context.Developers.FirstOrDefaultAsync(dev => dev.DeveloperId == developerId);
    }

    public async Task<bool> DeveloperExistsAsync(string developerName)
    {
        return await _context.Developers.AnyAsync(dev => dev.Name == developerName);
    }

    public async Task<bool> CreateDeveloperAsync(string developerName)
    {
        if (await DeveloperExistsAsync(developerName))
        {
            return false;
        }

        var newDeveloper = new DeveloperEntity
        {
            DeveloperId = Guid.NewGuid(),
            Name = developerName
        };

        _context.Developers.Add(newDeveloper);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<DeveloperEntity>> GetAllDevelopersAsync()
    {
        return await _context.Developers.ToListAsync();
    }

    public async Task<DeveloperEntity?> GetDeveloperByIdAsync(Guid developerId)
    {
        return await FindDeveloperByIdAsync(developerId);
    }

    public async Task<Guid?> GetIdDeveloperByNameAsync(string developerName)
    {
        var existingDeveloper = await _context.Developers.FirstOrDefaultAsync(dev => dev.Name == developerName);

        return existingDeveloper?.DeveloperId;
    }

    public async Task<bool> UpdateDeveloperAsync(Guid developerId, string newDeveloperName)
    {
        var existingDeveloper = await FindDeveloperByIdAsync(developerId);

        if (existingDeveloper == null)
        {
            return false;
        }

        existingDeveloper.Name = newDeveloperName;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteDeveloperAsync(Guid developerId)
    {
        var existingDeveloper = await FindDeveloperByIdAsync(developerId);

        if (existingDeveloper == null)
        {
            return false;
        }

        _context.Developers.Remove(existingDeveloper);
        await _context.SaveChangesAsync();

        return true;
    }
}