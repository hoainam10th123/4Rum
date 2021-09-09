using ForumAppCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Interfaces
{
    public interface ITagsLanguageRepository
    {
        void Add(Guid id, int[] tagsId);
        Task Update(CreateQuestionDto questionDto);
        Task<IEnumerable<TagsLanguageDto>> SearchAsync(string name);
        Task<IEnumerable<TagsLanguageDto>> GetTagsLanguageAsync();
    }
}
