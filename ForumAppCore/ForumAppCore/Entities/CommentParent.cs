using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Entities
{
    public class CommentParent
    {
        [Key]
        public Guid Id { get; set; }
        public string NoiDung { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public AppUser AppUser { get; set; }
        public Guid UserId { get; set; }

        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        public ICollection<ChildrentComment> ChildrentComments { get; set; }
    }

    public class ChildrentComment
    {
        [Key]
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public AppUser AppUser { get; set; }
        public Guid UserId { get; set; }
        public Guid ParentId { get; set; }
        public CommentParent Parent { get; set; }
    }
}
