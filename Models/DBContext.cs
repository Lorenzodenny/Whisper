using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Whisper.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Aphorisms> Aphorisms { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Conversations> Conversations { get; set; }
        public virtual DbSet<Emoticons> Emoticons { get; set; }
        public virtual DbSet<Friendships> Friendships { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Sponsors> Sponsors { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.Comments)
                .HasForeignKey(e => e.CommentId);

            //modelBuilder.Entity<Comments>()
            //    .HasMany(e => e.Reports1)
            //    .WithOptional(e => e.Comments1)
            //    .HasForeignKey(e => e.CommentId);

            modelBuilder.Entity<Conversations>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Conversations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Emoticons>()
                .HasMany(e => e.Likes)
                .WithRequired(e => e.Emoticons)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notifications>()
                .Property(e => e.NotificationType)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.CodiceFiscale)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Conversations)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User2Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Conversations1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.User2Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Friendships)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserMittenteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Friendships1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.UserRiceventeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Likes)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Notifications)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.TriggeredByUserID);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Notifications1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.ReportedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Sponsors)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}
