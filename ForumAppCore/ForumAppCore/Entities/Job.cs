using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Entities
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string JobName { get; set; }
        public string Address { get; set; }
        public string MoTa { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public ICollection<JobTag> JobTags { get; set; }
    }
}
