using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using ForumAppCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Interfaces
{
    public interface INotificationRepository
    {
        void Add(Notification model);
        Task SetRead(Guid userId);
        Task<PagedList<NotificationDto>> GetAll(NotificationParams notifiParams);
        Task<IEnumerable<NotificationDto>> GetUnRead(Guid userId);
    }
}
