using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class PublisherRepository : IPublisherRepository
{
    private readonly PixelBaseContext _context;

    public PublisherRepository(PixelBaseContext context)
    {
        _context = context;
    }

    public async Task<bool> PublisherExistsAsync(string publisherName)
    {
        return await _context.Publishers.AnyAsync(publisher => publisher.Name == publisherName);
    }

    public async Task<bool> CreatePublisherAsync(string publisherName)
    {
        if (await PublisherExistsAsync(publisherName))
        {
            return false;
        }

        var newPublisher = new PublisherEntity
        {
            PublisherId = Guid.NewGuid(),
            Name = publisherName,
        };

        _context.Publishers.Add(newPublisher);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<PublisherEntity>> GetAllPublishersAsync()
    {
        return await _context.Publishers.ToListAsync();
    }

    public async Task<Guid?> GetIdPublisherByNameAsync(string publisherName)
    {
        var existingPublisher = await _context.Publishers.FirstOrDefaultAsync(publisher => publisher.Name == publisherName);

        return existingPublisher?.PublisherId;
    }

    public async Task<PublisherEntity?> GetPublisherByIdAsync(Guid publisherId)
    {
        return await _context.Publishers.FindAsync(publisherId);
    }

    public async Task<bool> UpdatePublisherAsync(Guid publisherId, string newPublisherName)
    {
        var existingPublisher = await _context.Publishers.FindAsync(publisherId);
        if (existingPublisher == null) return false;

        existingPublisher.Name = newPublisherName;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletePublisherAsync(Guid publisherId)
    {
        var existingPublisher = await _context.Publishers.FindAsync(publisherId);
        if (existingPublisher == null) return false;

        _context.Publishers.Remove(existingPublisher);
        return await _context.SaveChangesAsync() > 0;
    }
}