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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<QuestionAtHome>> GetAllQuestion(PaginationParams questionParams)
        {
            var posts = _context.Questions.OrderByDescending(u => u.DatePosted);

            return await PagedList<QuestionAtHome>.CreateAsync(posts.ProjectTo<QuestionAtHome>(_mapper.ConfigurationProvider).AsNoTracking(), questionParams.PageNumber, questionParams.PageSize);
        }

        public async Task<QuestionDto> GetQuestionAsync(string questionId)
        {
            var id = Guid.Parse(questionId);
            return await _context.Questions.Where(p => p.Id == id)
                .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();//using Microsoft.EntityFrameworkCore;
        }

        public async Task<Question> GetQuestionAsyncDb(Guid questionId)
        {
            return await _context.Questions.Where(p => p.Id == questionId)       
                .SingleOrDefaultAsync();//using Microsoft.EntityFrameworkCore;
        }

        public async Task UpdateQuestion(CreateQuestionDto questionDto)
        {
            var q = await GetQuestionAsyncDb(Guid.Parse(questionDto.Id));
            q.Tittle = questionDto.Tittle;
            q.NoiDung = questionDto.NoiDung;

            var questionId = Guid.Parse(questionDto.Id);
            var tags = await _context.QuestionTags.Where(x => x.QuestionId == questionId).ToListAsync();
            if (questionDto.Tags != null)
            {
                _context.QuestionTags.RemoveRange(tags);
                foreach (var idTag in questionDto.Tags)//new update tags
                {
                    _context.QuestionTags.Add(new Entities.QuestionTag { QuestionId = questionId, TagId = idTag });
                }
            }
        }

        public async Task<Question> DeleteQuestion(string id)
        {
            var q_id = Guid.Parse(id);
            var post = await _context.Questions
                .SingleOrDefaultAsync(x => x.Id == q_id);

            if (post != null)
                _context.Questions.Remove(post);
            return post;
        }

        public void AddQuestion(Question question)
        {
            _context.Questions.Add(question);
        }

        public async Task<IEnumerable<QuestionAtHome>> SearchAsync(string name)
        {
            return await _context.Questions.Where(x => x.Tittle.ToLower().Contains(name.ToLower()))
                .ProjectTo<QuestionAtHome>(_mapper.ConfigurationProvider)
                .ToListAsync();//using Microsoft.EntityFrameworkCore;                              
        }

        public async Task<IEnumerable<SearchQuestionDto>> GetAll()
        {
            var list = await _context.Questions.ToListAsync();//using Microsoft.EntityFrameworkCore;
            return _mapper.Map<IEnumerable<SearchQuestionDto>>(list);
        }

        public async Task<IEnumerable<QuestionAtHome>> GetTopTen(int size)
        {
            var list = await _context.Questions.OrderByDescending(x=>x.DatePosted).Take(size).ToListAsync();//using Microsoft.EntityFrameworkCore;
            return _mapper.Map<IEnumerable<QuestionAtHome>>(list);
        }
    }
}
