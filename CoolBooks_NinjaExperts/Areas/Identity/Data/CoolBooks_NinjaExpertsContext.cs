using CoolBooks_NinjaExperts.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Models;


namespace CoolBooks_NinjaExperts.Data;

public class CoolBooks_NinjaExpertsContext : IdentityDbContext<UserInfo>
{
    public CoolBooks_NinjaExpertsContext(DbContextOptions<CoolBooks_NinjaExpertsContext> options)
        : base(options)
    {
        Database.EnsureCreated(); // Skapar databasen vid start av programmet
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Books>()
                    .HasMany(b => b.Authors)
                    .WithMany(a => a.Books)
                    .UsingEntity<AuthorsBooks>(ab => ab.HasOne<Authors>().WithMany(),
                    ab => ab.HasOne<Books>().WithMany());

        builder.Entity<Books>()
                    .HasMany(b => b.Genres)
                    .WithMany(a => a.Books)
                    .UsingEntity<BooksGenres>(bg => bg.HasOne<Genres>().WithMany(),
                    bg => bg.HasOne<Books>().WithMany());

        // LIKES AND DISLIKES TABLES
        builder.Entity<ReviewLikes>().HasKey(x => new { x.UserId, x.ReviewId });
        builder.Entity<ReviewDislikes>().HasKey(x => new { x.UserId, x.ReviewId });
        builder.Entity<CommentLikes>().HasKey(x => new { x.UserId, x.CommentId });
        builder.Entity<CommentDislikes>().HasKey(x => new { x.UserId, x.CommentId });
        builder.Entity<ReplyLikes>().HasKey(x => new { x.UserId, x.ReplyId });
        builder.Entity<ReplyDislikes>().HasKey(x => new { x.UserId, x.ReplyId });

        builder.Entity<ReviewLikes>()
            .HasOne(x => x.Review)
            .WithMany(x => x.ReviewLikes)
            .HasForeignKey(x => x.ReviewId);
        builder.Entity<ReviewDislikes>()
           .HasOne(x => x.Review)
           .WithMany(x => x.ReviewDislikes)
           .HasForeignKey(x => x.ReviewId);

        builder.Entity<CommentLikes>()
           .HasOne(x => x.Comment)
           .WithMany(x => x.CommentLikes)
           .HasForeignKey(x => x.CommentId);
        builder.Entity<CommentDislikes>()
           .HasOne(x => x.Comment)
           .WithMany(x => x.CommentDislikes)
           .HasForeignKey(x => x.CommentId);

        builder.Entity<ReplyLikes>()
           .HasOne(x => x.Reply)
           .WithMany(x => x.ReplyLikes)
           .HasForeignKey(x => x.ReplyId);
        builder.Entity<ReplyDislikes>()
           .HasOne(x => x.Reply)
           .WithMany(x => x.ReplyDislikes)
           .HasForeignKey(x => x.ReplyId);
        //---------------------------------------------------- 
        // Flagged Reviews/Comments/Replies
        builder.Entity<FlaggedReviews>().HasKey(x => new { x.UserId, x.ReviewId, x.FlaggedId });

        builder.Entity<FlaggedReviews>()
            .HasOne(x => x.Review)
            .WithMany(x => x.FlaggedReviews)
            .HasForeignKey(x => x.ReviewId);
        builder.Entity<FlaggedReviews>()
            .HasOne(x => x.User)
            .WithMany(x => x.FlaggedReviews)
            .HasForeignKey(x => x.UserId);
        builder.Entity<FlaggedReviews>()
            .HasOne(x => x.Flagged)
            .WithMany(x => x.FlaggedReviews)
            .HasForeignKey(x => x.FlaggedId);

        //----------------------------------------------------
        //Flagged Comments
        builder.Entity<FlaggedComments>().HasKey(x => new { x.UserId, x.FlaggedId, x.CommentId, x.ReviewId });

        builder.Entity<FlaggedComments>()
            .HasOne(x => x.Comments)
            .WithMany(x => x.FlaggedComments)
            .HasForeignKey(x => x.CommentId);
        //builder.Entity<FlaggedComments>()
        //     .HasOne(x => x.Review)
        //     .WithMany(x => x.)
        //     .HasForeignKey(x => x.ReviewId);
        builder.Entity<FlaggedComments>()
           .HasOne(x => x.User)
           .WithMany(x => x.FlaggedComments)
           .HasForeignKey(x => x.UserId);
        builder.Entity<FlaggedComments>()
            .HasOne(x => x.Flagged)
            .WithMany(x => x.FlaggedComments)
            .HasForeignKey(x => x.FlaggedId);

        // --------- seed database here :) -------------------

        //Users & Roles
        builder.SeedUsers();
        builder.SeedRoles();
        builder.SeedUserRoles();

        // Images 
        builder.SeedImages();
        // Authors
        builder.SeedAuthors();
        // Genres
        builder.SeedGenres();
        // Books
        builder.SeedBooks();
        // Reviews 
        builder.SeedBookReviews();
        // Comments
        builder.SeedReviewComments();
        //comment replies
        builder.SeedCommentReplies();
        //Flagged
        builder.SeedFlaggedTable();
        //builder.SeedFlaggedReviews();

        // Relationships
        builder.SeedAuthorBooks();
        builder.SeedBooksGenres();

        builder.SeedQuiz();
        builder.SeedQuestions();
        builder.SeedOptions();

    }

    public DbSet<Books> Books { get; set; }
    public DbSet<Authors> Authors { get; set; }
    public DbSet<Genres> Genres { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<UserInfo> UserInfo { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<Replies> Replies { get; set; }
    public DbSet<FlaggedReviews> FlaggedReviews { get; set; }   
    public DbSet<Flagged> Flagged { get; set; }
    public DbSet<ReviewLikes> ReviewLikes { get; set; }
    public DbSet<ReviewDislikes> ReviewDislikes { get; set; }
    public DbSet<FlaggedComments> FlaggedComments { get; set; }
    public DbSet<CommentLikes> CommentLikes { get; set; }
    public DbSet<CommentDislikes> CommentDislikes { get; set; }
    public DbSet<Quiz> Quiz { get; set; }
    public DbSet<Questions> Questions { get; set; }
    public DbSet<QuizOptions> QuizOptions { get; set; }
    public DbSet<QuizScoreboard> QuizScoreboards { get; set; }
}

