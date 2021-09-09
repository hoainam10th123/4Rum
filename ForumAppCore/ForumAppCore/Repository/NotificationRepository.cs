using AutoMapper;
using AutoMapper.QueryableExtensions;
using ForumAppCore.Data;
using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using ForumAppCore.Helpers;
using ForumAppCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Repository
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public NotificationRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(Notification model)
        {
            _context.Notifications.Add(model);
        }

        public async Task<IEnumerable<NotificationDto>> GetUnRead(Guid userId)
        {
            return await _context.Notifications.Where(x => x.UserId == userId && x.IsRead == false)
                .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task SetRead(Guid userId)
        {
            var list = await _context.Notifications.Where(x => x.UserId == userId && x.IsRead == false).ToListAsync();
            foreach (var notifi in list)
            {
                notifi.IsRead = true;
            }
        }

        public async Task<PagedList<NotificationDto>> GetAll(NotificationParams notifiParams)
        {
            var posts = _context.Notifications.Where(x => x.UserId == notifiParams.CurrentUserId).OrderByDescending(u => u.DateCreated);

            return await PagedList<NotificationDto>.CreateAsync(posts.ProjectTo<NotificationDto>(_mapper.ConfigurationProvider).AsNoTracking(), notifiParams.PageNumber, notifiParams.PageSize);
        }
    }
}
