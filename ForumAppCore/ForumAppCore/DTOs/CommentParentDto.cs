using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.DTOs
{
    public class CommentParentDto
    {
        public Guid Id { get; set; }
        public string NoiDung { get; set; }
        public DateTime DatePosted { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUrl { get; set; }
        public string UserName { get; set; }

        public Guid QuestionId { get; set; }
        public ICollection<CommentChildrentDto> ChildrentComments { get; set; }        
    }

    public class CommentChildrentDto
    {
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public DateTime DatePosted { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public Guid ParentId { get; set; }
    }
}
