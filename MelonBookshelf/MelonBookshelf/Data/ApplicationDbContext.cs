using MelonBookshelf.Data.DTO;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MelonBookshelf.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Upvote> Upvotes { get; set; }
        public DbSet<WantedResources> WantedResources { get; set; }
        public DbSet<ResourceComment> ResourceComments { get; set; }
        public DbSet<ResourceDownloadHistory> ResourceDownloadHistory { get; set; }
        public DbSet<BackgroundTask> BackgroundTasks { get; set; }
        public DbSet<CommentReply> CommentReplys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(r => r.Requests)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(r => r.Replys)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Request>()
                 .HasOne(r => r.Category)
                 .WithMany()
                 .HasForeignKey(r => r.CategoryId)
                 .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Resource>()
                 .HasOne(r => r.Category)
                 .WithMany()
                 .HasForeignKey(r => r.CategoryId)
                 .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Resource>()
                .HasMany(r => r.Comments)
                .WithOne(r => r.Resource)
                .HasForeignKey(r => r.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResourceComment>()
                .HasMany(r => r.CommentsReplies)
                .WithOne(c => c.ResourceComment)
                .HasForeignKey(k => k.ResourceCommentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
