using DataAccess.Entities.Users;
using DataAccess.Entities.Games;
using DataAccess.Entities.Reviews;
using DataAccess.Configurations.Users;
using DataAccess.Configurations.Games;
using DataAccess.Configurations.Reviews;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public partial class PixelBaseContext : DbContext
{
    public PixelBaseContext(DbContextOptions<PixelBaseContext> options) : base(options) {}

    public virtual DbSet<AddressEntity> Addresses { get; set; }
    public virtual DbSet<DeveloperEntity> Developers { get; set; }
    public virtual DbSet<FeatureEntity> Features { get; set; }
    public virtual DbSet<GameEntity> Games { get; set; }
    public virtual DbSet<GameImageEntity> GameImages { get; set; }
    public virtual DbSet<GenreEntity> Genres { get; set; }
    public virtual DbSet<PlatformEntity> Platforms { get; set; }
    public virtual DbSet<PublisherEntity> Publishers { get; set; }
    public virtual DbSet<ReviewEntity> Reviews { get; set; }
    public virtual DbSet<ReviewCommentEntity> ReviewComments { get; set; }
    public virtual DbSet<ReviewLikeEntity> ReviewLikes { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }
    public virtual DbSet<SystemRequirementEntity> SystemRequirements { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<UserDetailEntity> UserDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.ApplyConfiguration(new AddressesConfiguration());
        modelBuilder.ApplyConfiguration(new DeveloperConfiguration());
        modelBuilder.ApplyConfiguration(new FeatureConfiguration());
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new PlatformConfiguration());
        modelBuilder.ApplyConfiguration(new PublisherConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewCommentConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewLikeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new SystemRequirementConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserDetailConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}