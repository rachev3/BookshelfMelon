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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(r => r.Requests)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                
                
                //.HasOne(r => r.User)
                //.WithMany()
                //.HasForeignKey(r => r.UserId)
                //.OnDelete(DeleteBehavior.NoAction);


            //modelBuilder.Entity<User>()
            //   .HasMany(r => r.Requests)
            //   .WithOne(r => r.User)
            //   .HasForeignKey(r => r.UserId)
            //   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
