using ForumAppCore.Entities;
using ForumAppCore.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, Guid,
        IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<CommentParent> CommentParents { get; set; }
        public DbSet<ChildrentComment> ChildrentComments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobTag> JobTags { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<TagsLanguage> TagsLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            //user - question 1-n
            builder.Entity<Question>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.Questions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);//Xoa User - xoa luon question

            //user - notification. 1-n
            builder.Entity<Notification>()
                .HasOne(p => p.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Notification>()
                .HasOne(p => p.UserComment)
                .WithMany(u => u.NotificationComments)
                .HasForeignKey(p => p.UserCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            //user - CommentParent -- No Action. 1-n
            builder.Entity<CommentParent>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.CommentParents)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //user - ChildrenComment - No Action. 1-n
            builder.Entity<ChildrentComment>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.ChildrentComments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //Question - CommentParent. 1-n
            builder.Entity<CommentParent>()
                .HasOne(p => p.Question)
                .WithMany(u => u.CommentParents)
                .HasForeignKey(p => p.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<ChildrenComment>().HasKey(o => o.Id);

            //parentComment - ChildrenComment. 1-n
            builder.Entity<ChildrentComment>()
                .HasOne(p => p.Parent)
                .WithMany(u => u.ChildrentComments)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            //Question - TagsLanguage
            builder.Entity<QuestionTag>().HasKey(up => new { up.QuestionId, up.TagId });//Bảng trung gian của Question và TagLanguage

            builder.Entity<Question>()
                .HasMany(ur => ur.QuestionTags)
                .WithOne(u => u.Question)
                .HasForeignKey(ur => ur.QuestionId);

            builder.Entity<TagsLanguage>()
                .HasMany(ur => ur.QuestionTags)
                .WithOne(u => u.TagsLanguage)
                .HasForeignKey(ur => ur.TagId);

            //Job - TagsLanguage
            builder.Entity<JobTag>().HasKey(up => new { up.JobId, up.TagId });//Bảng trung gian của Job và TagLanguage

            builder.Entity<Job>()
                .HasMany(ur => ur.JobTags)
                .WithOne(u => u.Job)
                .HasForeignKey(ur => ur.JobId);

            builder.Entity<TagsLanguage>()
                .HasMany(ur => ur.JobTags)
                .WithOne(u => u.TagsLanguage)
                .HasForeignKey(ur => ur.TagId);

            //builder.Seed(); //Tag Laguages only migration-cmd run
        }
    }
}
