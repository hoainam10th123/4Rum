using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICommentRepository CommentRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        ITagsLanguageRepository TagsLanguageRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
