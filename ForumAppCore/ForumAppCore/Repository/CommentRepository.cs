using AutoMapper;
using AutoMapper.QueryableExtensions;
using ForumAppCore.Data;
using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using ForumAppCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CommentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddCommentChidren(ChildrentComment comment)
        {
            _context.ChildrentComments.Add(comment);
        }

        public void AddCommentParent(CommentParent comment)
        {
            _context.CommentParents.Add(comment);
        }

        public async Task<CommentParentDto> GetCommentParent(Guid id)
        {
            return await _context.CommentParents.Where(p => p.Id == id)
                .ProjectTo<CommentParentDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();//using Microsoft.EntityFrameworkCore;
        }

        public async Task<CommentChildrentDto> GetCommentChildrent(int id)
        {
            return await _context.ChildrentComments.Where(p => p.Id == id)
                .ProjectTo<CommentChildrentDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();//using Microsoft.EntityFrameworkCore;
        }

        public async Task<CommentParent> DeleteCommentParent(string id)
        {
            var q_id = Guid.Parse(id);
            var post = await _context.CommentParents
                .SingleOrDefaultAsync(x => x.Id == q_id);

            if (post != null)
                _context.CommentParents.Remove(post);
            return post;
        }

        public async Task<ChildrentComment> DeleteCommentChildrent(int id)
        {
            var post = await _context.ChildrentComments
                .SingleOrDefaultAsync(x => x.Id == id);

            if (post != null)
                _context.ChildrentComments.Remove(post);
            return post;
        }

        //comment.Id.GetType() == typeof(int)
        public void UpdateParent(CommentParent comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
        }

        public void UpdateChildrent(ChildrentComment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
        }
    }
}
