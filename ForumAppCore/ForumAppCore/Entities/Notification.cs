using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string NoiDung { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid UserCommentId { get; set; }
        public AppUser UserComment { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public bool IsRead { get; set; } = false;//false chưa đọc, true: đã đọc
        public Guid? CommentParentId { get; set; }
        public int? CommentChildrentId { get; set; }
    }
}
