using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WHATEVER_API2.Models
{
    public partial class WHATEVERContext : DbContext
    {
        public WHATEVERContext()
        {
        }

        public WHATEVERContext(DbContextOptions<WHATEVERContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<HistoryArticle> HistoryArticles { get; set; } = null!;
        public virtual DbSet<HistoryUser> HistoryUsers { get; set; } = null!;
        public virtual DbSet<LanguageProgramming> LanguageProgrammings { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StatusArticle> StatusArticles { get; set; } = null!;
        public virtual DbSet<StatusUser> StatusUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-7NUBAMDH\\SQLEXPRESS;Initial Catalog=WHATEVER;Persist Security Info=True;User ID=sa;Password=1111");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.IdArticle);

                entity.ToTable("Article");

                entity.Property(e => e.IdArticle).HasColumnName("ID_Article");

                entity.Property(e => e.DateTimeArticle)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_Article");

                entity.Property(e => e.Header)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageProgrammingId).HasColumnName("Language_Programming_ID");

                entity.Property(e => e.StatusArticleId).HasColumnName("Status_Article_ID");

                entity.Property(e => e.Text).IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => e.IdFavorites);

                entity.HasIndex(e => new { e.UserId, e.ArticleId }, "UQ_User_Articles")
                    .IsUnique();

                entity.Property(e => e.IdFavorites).HasColumnName("ID_Favorites");

                entity.Property(e => e.ArticleId).HasColumnName("Article_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

            });

            modelBuilder.Entity<HistoryArticle>(entity =>
            {
                entity.HasKey(e => e.IdHistoryArticle);

                entity.ToTable("History_Article");

                entity.Property(e => e.IdHistoryArticle).HasColumnName("ID_History_Article");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeArticleHistoryArticle)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_Article_History_Article");

                entity.Property(e => e.DateTimeHistoryArticle)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_History_Article");

                entity.Property(e => e.HeaderHistoryArticle)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Header_History_Article");

                entity.Property(e => e.LanguageProgrammingHistoryArticle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Language_Programming_History_Article");

                entity.Property(e => e.LoginHistoryArticle)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Login_History_Article");

                entity.Property(e => e.TextHistoryArticle)
                    .IsUnicode(false)
                    .HasColumnName("Text_History_Article");
            });

            modelBuilder.Entity<HistoryUser>(entity =>
            {
                entity.HasKey(e => e.IdHistoryUser);

                entity.ToTable("History_User");

                entity.Property(e => e.IdHistoryUser).HasColumnName("ID_History_User");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeHistoryUser)
                    .HasColumnType("datetime")
                    .HasColumnName("DateTime_History_User");

                entity.Property(e => e.InfoUser)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Info_User");
            });

            modelBuilder.Entity<LanguageProgramming>(entity =>
            {
                entity.HasKey(e => e.IdLanguageProgramming);

                entity.ToTable("Language_Programming");

                entity.HasIndex(e => e.LanguageProgramming1, "UQ_Language_Programming")
                    .IsUnique();

                entity.Property(e => e.IdLanguageProgramming).HasColumnName("ID_Language_Programming");

                entity.Property(e => e.LanguageProgramming1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Language_Programming");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.HasIndex(e => e.Role1, "UQ_Role")
                    .IsUnique();

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.Role1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<StatusArticle>(entity =>
            {
                entity.HasKey(e => e.IdStatusArticle);

                entity.ToTable("Status_Article");

                entity.HasIndex(e => e.StatusArticle1, "UQ_Status_Article")
                    .IsUnique();

                entity.Property(e => e.IdStatusArticle).HasColumnName("ID_Status_Article");

                entity.Property(e => e.StatusArticle1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Status_Article");
            });

            modelBuilder.Entity<StatusUser>(entity =>
            {
                entity.HasKey(e => e.IdStatusUser);

                entity.ToTable("Status_User");

                entity.HasIndex(e => e.StatusUser1, "UQ_Status")
                    .IsUnique();

                entity.Property(e => e.IdStatusUser).HasColumnName("ID_Status_User");

                entity.Property(e => e.StatusUser1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Status_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ_Email")
                    .IsUnique();

                entity.HasIndex(e => e.LoginUser, "UQ_Login")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LoginUser)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Login_User");

                entity.Property(e => e.PasswordUser)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Password_User");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.StatusUserId).HasColumnName("Status_User_ID");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
