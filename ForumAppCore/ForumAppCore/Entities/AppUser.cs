using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public DateTime LastActive { get; set; } = DateTime.Now;
        public DateTime? DateOfBirth { get; set; }
        public string DisplayName { get; set; }
        public string? PhotoUrl { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Notification> NotificationComments { get; set; }
        public ICollection<CommentParent> CommentParents { get; set; }
        public ICollection<ChildrentComment> ChildrentComments { get; set; }
    }
}
