using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Data;

public partial class PixelBaseContext : DbContext
{
    public PixelBaseContext()
    {
    }

    public PixelBaseContext(DbContextOptions<PixelBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameImage> GameImages { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ReviewComment> ReviewComments { get; set; }

    public virtual DbSet<ReviewLike> ReviewLikes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemRequirement> SystemRequirements { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=PixelBase;Username=this;Password=thisfozz");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("addresses_pkey");

            entity.ToTable("addresses");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("address_id");
            entity.Property(e => e.Address1)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .HasColumnName("country");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .HasColumnName("postal_code");
            entity.Property(e => e.Region)
                .HasMaxLength(30)
                .HasColumnName("region");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("addresses_user_id_fkey");
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.DeveloperId).HasName("developers_pkey");

            entity.ToTable("developers");

            entity.HasIndex(e => e.Name, "developers_name_key").IsUnique();

            entity.Property(e => e.DeveloperId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("developer_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("features_pkey");

            entity.ToTable("features");

            entity.HasIndex(e => e.Name, "features_name_key").IsUnique();

            entity.Property(e => e.FeatureId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("feature_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("game_pkey");

            entity.ToTable("game");

            entity.Property(e => e.GameId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("game_id");
            entity.Property(e => e.CoverUrl).HasColumnName("cover_url");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DeveloperId).HasColumnName("developer_id");
            entity.Property(e => e.PlatformId).HasColumnName("platform_id");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.Rating)
                .HasPrecision(5, 2)
                .HasColumnName("rating");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.ReleaseDateStr)
                .HasMaxLength(20)
                .HasColumnName("release_date_str");
            entity.Property(e => e.ReviewCount)
                .HasDefaultValue(0)
                .HasColumnName("review_count");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Developer).WithMany(p => p.Games)
                .HasForeignKey(d => d.DeveloperId)
                .HasConstraintName("game_developer_id_fkey");

            entity.HasOne(d => d.Platform).WithMany(p => p.Games)
                .HasForeignKey(d => d.PlatformId)
                .HasConstraintName("game_platform_id_fkey");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Games)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("game_publisher_id_fkey");

            entity.HasMany(d => d.Features).WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GameFeature",
                    r => r.HasOne<Feature>().WithMany()
                        .HasForeignKey("FeatureId")
                        .HasConstraintName("game_features_feature_id_fkey"),
                    l => l.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("game_features_game_id_fkey"),
                    j =>
                    {
                        j.HasKey("GameId", "FeatureId").HasName("game_features_pkey");
                        j.ToTable("game_features");
                        j.IndexerProperty<Guid>("GameId").HasColumnName("game_id");
                        j.IndexerProperty<Guid>("FeatureId").HasColumnName("feature_id");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GameGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("game_genres_genre_id_fkey"),
                    l => l.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("game_genres_game_id_fkey"),
                    j =>
                    {
                        j.HasKey("GameId", "GenreId").HasName("game_genres_pkey");
                        j.ToTable("game_genres");
                        j.IndexerProperty<Guid>("GameId").HasColumnName("game_id");
                        j.IndexerProperty<Guid>("GenreId").HasColumnName("genre_id");
                    });

            entity.HasMany(d => d.Platforms).WithMany(p => p.GamesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "GamePlatform",
                    r => r.HasOne<Platform>().WithMany()
                        .HasForeignKey("PlatformId")
                        .HasConstraintName("game_platforms_platform_id_fkey"),
                    l => l.HasOne<Game>().WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("game_platforms_game_id_fkey"),
                    j =>
                    {
                        j.HasKey("GameId", "PlatformId").HasName("game_platforms_pkey");
                        j.ToTable("game_platforms");
                        j.IndexerProperty<Guid>("GameId").HasColumnName("game_id");
                        j.IndexerProperty<Guid>("PlatformId").HasColumnName("platform_id");
                    });
        });

        modelBuilder.Entity<GameImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("game_images_pkey");

            entity.ToTable("game_images");

            entity.Property(e => e.ImageId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("image_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");

            entity.HasOne(d => d.Game).WithMany(p => p.GameImages)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("game_images_game_id_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.HasIndex(e => e.Name, "genres_name_key").IsUnique();

            entity.Property(e => e.GenreId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("genre_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.PlatformId).HasName("platforms_pkey");

            entity.ToTable("platforms");

            entity.HasIndex(e => e.Name, "platforms_name_key").IsUnique();

            entity.Property(e => e.PlatformId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("platform_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("publishers_pkey");

            entity.ToTable("publishers");

            entity.HasIndex(e => e.Name, "publishers_name_key").IsUnique();

            entity.Property(e => e.PublisherId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("publisher_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.GameId }).HasName("reviews_pkey");

            entity.ToTable("reviews");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Recommend).HasColumnName("recommend");
            entity.Property(e => e.ReviewText).HasColumnName("review_text");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Game).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("reviews_game_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("reviews_user_id_fkey");
        });

        modelBuilder.Entity<ReviewComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("review_comments_pkey");

            entity.ToTable("review_comments");

            entity.Property(e => e.CommentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("comment_id");
            entity.Property(e => e.CommentText).HasColumnName("comment_text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ReviewGameId).HasColumnName("review_game_id");
            entity.Property(e => e.ReviewUserId).HasColumnName("review_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("review_comments_user_id_fkey");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewComments)
                .HasForeignKey(d => new { d.ReviewUserId, d.ReviewGameId })
                .HasConstraintName("review_comments_review_user_id_review_game_id_fkey");
        });

        modelBuilder.Entity<ReviewLike>(entity =>
        {
            entity.HasKey(e => new { e.ReviewUserId, e.ReviewGameId, e.UserId }).HasName("review_likes_pkey");

            entity.ToTable("review_likes");

            entity.Property(e => e.ReviewUserId).HasColumnName("review_user_id");
            entity.Property(e => e.ReviewGameId).HasColumnName("review_game_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.IsLike).HasColumnName("is_like");

            entity.HasOne(d => d.User).WithMany(p => p.ReviewLikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("review_likes_user_id_fkey");

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewLikes)
                .HasForeignKey(d => new { d.ReviewUserId, d.ReviewGameId })
                .HasConstraintName("review_likes_review_user_id_review_game_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(30)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SystemRequirement>(entity =>
        {
            entity.HasKey(e => e.RequirementId).HasName("system_requirements_pkey");

            entity.ToTable("system_requirements");

            entity.Property(e => e.RequirementId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("requirement_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.MinRequirements).HasColumnName("min_requirements");
            entity.Property(e => e.RecommendedRequirements).HasColumnName("recommended_requirements");

            entity.HasOne(d => d.Game).WithMany(p => p.SystemRequirements)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("system_requirements_game_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.DisplayName, "users_display_name_key").IsUnique();

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Login, "users_login_key").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.AccountCreationDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("account_creation_date");
            entity.Property(e => e.Balance)
                .HasPrecision(15, 2)
                .HasColumnName("balance");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(20)
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailsId).HasName("user_details_pkey");

            entity.ToTable("user_details");

            entity.Property(e => e.UserDetailsId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_details_id");
            entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("last_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_details_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}