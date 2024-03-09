﻿using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess.Data
{
    public class DataContext : IdentityDbContext<AppUser,
                                                AppRole,
                                                int,
                                                IdentityUserClaim<int>,
                                                AppUserRole,
                                                IdentityUserLogin<int>,
                                                IdentityRoleClaim<int>,
                                                IdentityUserToken<int>
                                                >
    {

        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> AppUserRole { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<ArtworkComment> ArtworkComments { get; set; }
        public DbSet<ArtworkLike> ArtworkLikes { get; set; }
        public DbSet<CommissionRequest> CommissionRequests { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<CommissionImage> CommissionImages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserImage> UserImages{ get; set; }
        public DbSet<ArtworkImage> ArtworkImages { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<CommissionStatus> CommissionStatus { get; set; }
        public DbSet<Connection> Connections { get; set; }


        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer(GetConnectionString());



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<ArtworkLike>()
                .HasKey(k => new { k.ArtworkId, k.AppUserId });
            builder.Entity<ArtworkLike>()
                .HasOne(a => a.Artwork)
                .WithMany(a => a.Likes)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ArtworkLike>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.LikedArtwork)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ArtworkComment>()
                .HasOne(a => a.Artwork)
                .WithMany(a => a.Comments)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ArtworkComment>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Purchase>()
                .HasKey(k => new { k.ArtworkId, k.AppUserId });
            builder.Entity<Purchase>()
                .HasOne(p => p.Artwork)
                .WithMany(a => a.Purchases)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Purchase>()
                .HasOne(p => p.AppUser)
                .WithMany(a => a.Purchases)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollow>()
                .HasKey(k => new { k.SourceUserId, k.TargetUserId });
            builder.Entity<UserFollow>()
                .HasOne(fl => fl.SourceUser)
                .WithMany(u => u.FollowedUsers)
                .HasForeignKey(u => u.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserFollow>()
                .HasOne(fl => fl.TargetUser)
                .WithMany(u => u.IsFollowedByUsers)
                .HasForeignKey(u => u.TargetUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Transaction>()
                .HasKey(k => new { k.SenderId, k.ReceiverId });
            builder.Entity<Transaction>()
                .HasOne(t => t.Sender)
                .WithMany(u => u.TransactionSents)
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Transaction>()
            .HasOne(t => t.Receiver)
            .WithMany(u => u.TransactionReceived)
            .HasForeignKey(t => t.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CommissionRequest>()
                .HasOne(c => c.Sender)
                .WithMany(u => u.CommissionSent)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<CommissionRequest>()
                .HasOne(c => c.Receiver)
                .WithMany(u => u.CommissionReceived)
                .HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Artwork>()
                .HasOne(x => x.ArtworkImage)
                .WithOne(x => x.Artwork)
                .OnDelete(DeleteBehavior.NoAction);
			builder.Entity<AppUser>()
				.HasOne(x => x.UserImage)
				.WithOne(x => x.AppUser)
				.OnDelete(DeleteBehavior.NoAction);
		}
    }
}
