using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using ForumAppCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Interfaces
{
    public interface IQuestionRepository
    {
        void AddQuestion(Question question);
        Task<Question> DeleteQuestion(string id);
        Task UpdateQuestion(CreateQuestionDto questionDto);
        Task<IEnumerable<SearchQuestionDto>> GetAll();
        Task<QuestionDto> GetQuestionAsync(string questionId);
        Task<Question> GetQuestionAsyncDb(Guid questionId);
        Task<IEnumerable<QuestionAtHome>> SearchAsync(string name);
        Task<PagedList<QuestionAtHome>> GetAllQuestion(PaginationParams questionParams);
        Task<IEnumerable<QuestionAtHome>> GetTopTen(int size);
    }
}
