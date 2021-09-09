using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Helpers
{
    public class NotificationParams : PaginationParams
    {
        public Guid CurrentUserId { get; set; }
    }
}
