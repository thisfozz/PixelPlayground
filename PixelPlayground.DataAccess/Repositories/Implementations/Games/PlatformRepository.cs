﻿using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class PlatformRepository : IPlatformRepository
{
    private readonly PixelBaseContext _context;

    public PlatformRepository(PixelBaseContext context)
    {
        _context = context;
    }

    private async Task<PlatformEntity?> FindPlatformByIdAsync(Guid platformId)
    {
        return await _context.Platforms.FirstOrDefaultAsync(platform => platform.PlatformId == platformId);
    }

    public async Task<bool> PlatformExistsAsync(string platformName)
    {
        return await _context.Platforms.AnyAsync(platform => platform.Name == platformName);
    }

    public async Task<bool> CreatePlatformAsync(string platformName)
    {
        if (await PlatformExistsAsync(platformName))
        {
            return false;
        }

        var newPlatform = new PlatformEntity
        {
            PlatformId = Guid.NewGuid(),
            Name = platformName
        };

        _context.Platforms.Add(newPlatform);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<PlatformEntity>> GetAllPlatformsAsync()
    {
        return await _context.Platforms.ToListAsync();
    }

    public async Task<Guid?> GetIdPlatformByNameAsync(string platformName)
    {
        var existingPlatform = await _context.Platforms.FirstOrDefaultAsync(platform => platform.Name == platformName);

        return existingPlatform?.PlatformId;
    }

    public async Task<PlatformEntity?> GetPlatformByIdAsync(Guid platformId)
    {
        return await FindPlatformByIdAsync(platformId);
    }

    public async Task<bool> UpdatePlatformAsync(Guid platformId, string newPlatformName)
    {
        var existingPlatform = await FindPlatformByIdAsync(platformId);

        if (existingPlatform == null)
        {
            return false;
        }

        existingPlatform.Name = newPlatformName;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePlatformAsync(Guid platformId)
    {
        var existingPlatform = await FindPlatformByIdAsync(platformId);

        if (existingPlatform == null)
        {
            return false;
        }

        _context.Platforms.Remove(existingPlatform);
        await _context.SaveChangesAsync();

        return true;
    }
}