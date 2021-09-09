using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Interfaces
{
    public interface ICommentRepository
    {
        void AddCommentParent(CommentParent comment);
        void AddCommentChidren(ChildrentComment comment);
        void UpdateParent(CommentParent comment);
        void UpdateChildrent(ChildrentComment comment);
        Task<CommentParentDto> GetCommentParent(Guid id);
        Task<CommentChildrentDto> GetCommentChildrent(int id);
        Task<CommentParent> DeleteCommentParent(string id);
        Task<ChildrentComment> DeleteCommentChildrent(int id);
    }
}
