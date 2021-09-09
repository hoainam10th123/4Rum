using AutoMapper;
using AutoMapper.QueryableExtensions;
using ForumAppCore.Data;
using ForumAppCore.DTOs;
using ForumAppCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Repository
{
    public class TagsLanguageRepository : ITagsLanguageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TagsLanguageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagsLanguageDto>> GetTagsLanguageAsync()
        {
            return await _context.TagsLanguages
                .ProjectTo<TagsLanguageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();//using Microsoft.EntityFrameworkCore;
        }

        public void Add(Guid id, int[] tagsId)
        {
            if(tagsId != null)
                for(var i = 0; i < tagsId.Length; i++)
                {
                    _context.QuestionTags.Add(new Entities.QuestionTag { QuestionId = id, TagId = tagsId[i] });
                }
        }

        public async Task Update(CreateQuestionDto questionDto)
        {
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

        public async Task<IEnumerable<TagsLanguageDto>> SearchAsync(string name)
        {
            return await _context.TagsLanguages.Where(x=>x.Name.ToLower().Contains(name.ToLower()))
                .ProjectTo<TagsLanguageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();//using Microsoft.EntityFrameworkCore;
        }
    }
}
