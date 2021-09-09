using AutoMapper;
using ForumAppCore.Interfaces;
using ForumAppCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        DataContext _context;
        IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);
        public ICommentRepository CommentRepository => new CommentRepository(_context, _mapper);
        public INotificationRepository NotificationRepository => new NotificationRepository(_context, _mapper);
        public IQuestionRepository QuestionRepository => new QuestionRepository(_context, _mapper);
        public ITagsLanguageRepository TagsLanguageRepository => new TagsLanguageRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
