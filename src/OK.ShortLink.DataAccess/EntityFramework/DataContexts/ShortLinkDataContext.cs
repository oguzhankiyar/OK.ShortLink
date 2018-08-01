using Microsoft.EntityFrameworkCore;
using OK.ShortLink.Common.Entities;

namespace OK.ShortLink.DataAccess.EntityFramework.DataContexts
{
    public class ShortLinkDataContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<LinkEntity> Links { get; set; }

        public virtual DbSet<VisitorEntity> Visitors { get; set; }

        public virtual DbSet<LogEntity> Logs { get; set; }

        public ShortLinkDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Build Users Table

            modelBuilder.Entity<UserEntity>()
                        .ToTable("Users")
                        .HasKey(x => x.Id);

            modelBuilder.Entity<UserEntity>()
                        .Property(x => x.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();

            modelBuilder.Entity<UserEntity>()
                        .Property(x => x.Username)
                        .IsRequired();

            modelBuilder.Entity<UserEntity>()
                        .Property(x => x.Password)
                        .IsRequired();

            modelBuilder.Entity<UserEntity>()
                        .Property(x => x.IsActive)
                        .IsRequired();

            modelBuilder.Entity<UserEntity>()
                        .Property(x => x.CreatedDate)
                        .IsRequired();

            modelBuilder.Entity<UserEntity>()
                        .Property(x => x.UpdatedDate)
                        .IsRequired();

            #endregion

            #region Build Links Table

            modelBuilder.Entity<LinkEntity>()
                        .ToTable("Links")
                        .HasKey(x => x.Id);

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.UserId)
                        .IsRequired();

            modelBuilder.Entity<LinkEntity>()
                        .HasOne(x => x.User)
                        .WithMany()
                        .HasForeignKey(nameof(LinkEntity.UserId))
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.Name)
                        .IsRequired();

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.Description)
                        .IsRequired();

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.OriginalUrl)
                        .IsRequired();

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.ShortUrl)
                        .IsRequired();

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.IsActive)
                        .IsRequired();

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.CreatedDate)
                        .IsRequired();

            modelBuilder.Entity<LinkEntity>()
                        .Property(x => x.UpdatedDate)
                        .IsRequired();

            #endregion

            #region Build Visitors Table

            modelBuilder.Entity<VisitorEntity>()
                        .ToTable("Visitors")
                        .HasKey(x => x.Id);

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.LinkId)
                        .IsRequired();

            modelBuilder.Entity<VisitorEntity>()
                        .HasOne(x => x.Link)
                        .WithMany()
                        .HasForeignKey(nameof(VisitorEntity.LinkId))
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.IPAddress)
                        .IsRequired();

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.UserAgent)
                        .IsRequired();

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.OSInfo)
                        .IsRequired();

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.BrowserInfo)
                        .IsRequired();

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.DeviceInfo)
                        .IsRequired();

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.CreatedDate)
                        .IsRequired();

            modelBuilder.Entity<VisitorEntity>()
                        .Property(x => x.UpdatedDate)
                        .IsRequired();

            #endregion

            #region Build Log Table

            modelBuilder.Entity<LogEntity>()
                        .ToTable("Logs")
                        .HasKey(x => x.Id);

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.Level)
                        .IsRequired();

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.Thread)
                        .IsRequired();

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.Channel)
                        .IsRequired();

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.RequestId)
                        .IsRequired();

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.UserId)
                        .IsRequired(false);

            modelBuilder.Entity<LogEntity>()
                        .HasOne(x => x.User)
                        .WithMany()
                        .HasForeignKey(nameof(LogEntity.UserId))
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.Source)
                        .IsRequired();

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.Message)
                        .IsRequired();

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.Data)
                        .IsRequired(false);

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.Exception)
                        .IsRequired(false);

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.IPAddress)
                        .IsRequired(false);

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.UserAgent)
                        .IsRequired(false);

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.CreatedDate)
                        .IsRequired();

            modelBuilder.Entity<LogEntity>()
                        .Property(x => x.UpdatedDate)
                        .IsRequired();

            #endregion
        }
    }
}