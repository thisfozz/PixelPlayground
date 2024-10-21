using DataAccess.Contexts;
using DataAccess.Entities.Reviews;
using DataAccess.Repositories.Contracts.Interfaces.Reviews;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Reviews;

public class ReviewRepository : IReviewRepository
{
    private readonly PixelBaseContext _context;

    public ReviewRepository(PixelBaseContext context)
    {
        _context = context;
    }
    public async Task<bool> AddReviewWithLikeAsync(Guid gameId, ReviewEntity review, bool isLike)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return false;

        var existingReview = await _context.Reviews.FindAsync(review.UserId, gameId);
        if (existingReview != null) return false;

        review.GameId = gameId;
        review.CreatedAt = DateTime.UtcNow;
        review.UpdatedAt = DateTime.UtcNow;
        await _context.Reviews.AddAsync(review);

        var reviewLike = new ReviewLikeEntity
        {
            ReviewUserId = review.UserId,
            ReviewGameId = gameId,
            UserId = review.UserId,
            IsLike = isLike
        };

        await _context.ReviewLikes.AddAsync(reviewLike);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ReviewEntity>> GetReviewsByGameIdAsync(Guid gameId)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return Enumerable.Empty<ReviewEntity>();

        return await _context.Reviews
            .Where(review => review.GameId == gameId)
            .Include(review => review.User)
            .Include(review => review.User.UserDetails)
            .ToListAsync();
    }

    public async Task<ReviewEntity?> GetReviewByUserAndGameIdAsync(Guid gameId, Guid userId)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return null;

        return await _context.Reviews
            .Where(review => review.UserId == userId && review.GameId == gameId)
            .Include(review => review.User)
            .Include(review => review.User.UserDetails)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateReviewAsync(Guid gameId, ReviewEntity review, bool isLike)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return false;

        var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.UserId == review.UserId && r.GameId == gameId);
        if (existingReview == null) return false;

        existingReview.ReviewText = review.ReviewText;
        existingReview.Recommend = review.Recommend;
        existingReview.UpdatedAt = DateTime.UtcNow;

        var existingLike = await _context.ReviewLikes.FirstOrDefaultAsync(l => l.UserId == review.UserId && l.ReviewGameId == gameId);
        if(existingLike == null) return false;

        existingLike.IsLike = isLike;

        _context.ReviewLikes.Update(existingLike);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteReviewAsync(Guid gameId, Guid userId)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return false;

        var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.GameId == gameId);
        if (existingReview == null) return false;

        _context.Reviews.Remove(existingReview);
        return await _context.SaveChangesAsync() > 0;
    }
}