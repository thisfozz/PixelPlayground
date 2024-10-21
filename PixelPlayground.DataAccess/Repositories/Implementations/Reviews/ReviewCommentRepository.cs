using DataAccess.Contexts;
using DataAccess.Entities.Reviews;
using DataAccess.Repositories.Contracts.Interfaces.Reviews;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Reviews;

public class ReviewCommentRepository : IReviewCommentRepository
{
    private readonly PixelBaseContext _context;

    public ReviewCommentRepository(PixelBaseContext context)
    {
        _context = context;
    }
    public async Task<bool> AddCommentAsync(Guid gameId, Guid reviewId, ReviewCommentEntity comment)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return false;

        var existingReview = await _context.Reviews.FindAsync(reviewId);
        if (existingReview == null) return false;

        comment.ReviewUserId = existingReview.UserId;
        comment.ReviewGameId = existingReview.GameId;
        comment.CreatedAt = DateTime.UtcNow;

        await _context.ReviewComments.AddAsync(comment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ReviewCommentEntity>> GetCommentsByReviewAsync(Guid gameId, Guid reviewUserId, Guid reviewId)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return Enumerable.Empty<ReviewCommentEntity>();

        var existingReview = await _context.Reviews.FindAsync(reviewId);
        if (existingReview == null) return Enumerable.Empty<ReviewCommentEntity>();

        return await _context.ReviewComments
            .Where(comment => comment.ReviewGameId == gameId && comment.ReviewUserId == reviewId)
            .Include(comment => comment.User)
            .Include(comment => comment.User.UserDetails)
            .ToListAsync();
    }

    public async Task<bool> DeleteCommentAsync(Guid gameId, Guid reviewId, Guid commentId)
    {
        var existingGame = await _context.Games.FindAsync(gameId);
        if (existingGame == null) return false;

        var existingReview = await _context.Reviews.FindAsync(reviewId);
        if (existingReview == null) return false;

        var existingComment = await _context.ReviewComments.Where(c => c.ReviewGameId == gameId && c.ReviewUserId == reviewId && c.CommentId == commentId).FirstOrDefaultAsync();
        if (existingComment == null) return false;

        _context.ReviewComments.Remove(existingComment);
        return await _context.SaveChangesAsync() > 0;
    }
}