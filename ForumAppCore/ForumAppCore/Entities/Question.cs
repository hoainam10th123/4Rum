using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Entities
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public string Tittle { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public long ViewCount { get; set; }
        public string NoiDung { get; set; }
        public AppUser AppUser { get; set; }
        public Guid UserId { get; set; }
        public ICollection<QuestionTag> QuestionTags { get; set; }
        public ICollection<CommentParent> CommentParents { get; set; }//Question - comment - cascade
    }
}
