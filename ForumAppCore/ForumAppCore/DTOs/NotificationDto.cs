using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string NoiDung { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }
        public Guid UserCommentId { get; set; }
        public string DisplayNameComment { get; set; }
        public string PhotoUrlComment { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsRead { get; set; }//false chưa đọc, true: đã đọc
        public Guid CommentParentId { get; set; }
        public int CommentChildrentId { get; set; }
    }
}
