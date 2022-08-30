using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Realworlddotnet.Core.Entities;

namespace Realworlddotnet.Data.Contexts
{
    public partial class ConduitContext : DbContext
    {
        public ConduitContext()
        {
        }

        public ConduitContext(DbContextOptions<ConduitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<ArticleFavorite> ArticleFavorites { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<FollowedUser> FollowedUsers { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Conduit;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasIndex(e => e.AuthorUsername, "IX_Articles_AuthorUsername");

                entity.HasIndex(e => e.Slug, "IX_Articles_Slug")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.AuthorUsernameNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.AuthorUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.Articles)
                    .UsingEntity<Dictionary<string, object>>(
                        "ArticleTag",
                        l => l.HasOne<Tag>().WithMany().HasForeignKey("TagsId"),
                        r => r.HasOne<Article>().WithMany().HasForeignKey("ArticlesId"),
                        j =>
                        {
                            j.HasKey("ArticlesId", "TagsId");

                            j.ToTable("ArticleTag");

                            j.HasIndex(new[] { "TagsId" }, "IX_ArticleTag_TagsId");
                        });
            });

            modelBuilder.Entity<ArticleFavorite>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.Username });

                entity.HasIndex(e => e.Username, "IX_ArticleFavorites_Username");

                entity.Property(e => e.ArticleFavoriteId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleFavorites)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.ArticleFavorites)
                    .HasForeignKey(d => d.Username);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.ArticleId, "IX_Comments_ArticleId");

                entity.HasIndex(e => e.Username, "IX_Comments_Username");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FollowedUser>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.FollowerUsername });

                entity.HasIndex(e => e.FollowerUsername, "IX_FollowedUsers_FollowerUsername");

                entity.Property(e => e.FollowedUsersId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.FollowerUsernameNavigation)
                    .WithMany(p => p.FollowedUserFollowerUsernameNavigations)
                    .HasForeignKey(d => d.FollowerUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.FollowedUserUsernameNavigations)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.HasIndex(e => e.Email, "IX_Users_Email")
                    .IsUnique();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
