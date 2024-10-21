using DataAccess.Contexts;
using DataAccess.Entities.Users;
using DataAccess.Repositories.Contracts.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Users;

public class AddressRepository : IAddressRepository
{
    private readonly PixelBaseContext _context;

    public AddressRepository(PixelBaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AddressEntity>> GetAddressesByUserIdAsync(Guid userId)
    {
        return await _context.Addresses.Where(address => address.UserId == userId).ToListAsync();
    }

    public async Task<AddressEntity?> GetAddressByIdAsync(Guid addressId)
    {
        return await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
    }

    public async Task<bool> AddAddressAsync(AddressEntity address)
    {
        if (address == null) return false;

        var userExists = await _context.Users.AnyAsync(user => user.UserId == address.UserId);
        if (!userExists) return false;

        await _context.Addresses.AddAsync(address);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAddressAsync(Guid userId, AddressEntity newAddress)
    {
        if (newAddress == null) return false;

        var existingAddress = await _context.Addresses.FirstOrDefaultAsync(address => address.AddressId == newAddress.AddressId);
        if (existingAddress == null || existingAddress.UserId != userId) return false;

        existingAddress.Country = newAddress.Country;
        existingAddress.City = newAddress.City;
        existingAddress.Region = newAddress.Region;
        existingAddress.Address = newAddress.Address;
        existingAddress.PostalCode = newAddress.PostalCode;
        existingAddress.PhoneNumber = newAddress.PhoneNumber;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAddressByIdAsync(Guid addressId)
    {
        var address = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
        if (address == null) return false;

        _context.Addresses.Remove(address);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAllAddressesByUserIdAsync(Guid userId)
    {
        var addresses = await _context.Addresses.Where(address => address.UserId == userId).ToListAsync();

        _context.Addresses.RemoveRange(addresses);
        return await _context.SaveChangesAsync() > 0;
    }
}