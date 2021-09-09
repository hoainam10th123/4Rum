using AutoMapper;
using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using ForumAppCore.Extensions;
using ForumAppCore.Helpers;
using ForumAppCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Controllers
{
    [Authorize]
    public class QuestionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        IConfiguration _config;

        public QuestionController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAtHome>>> GetQuestions([FromQuery] PaginationParams questionParams)
        {
            var posts = await _unitOfWork.QuestionRepository.GetAllQuestion(questionParams);
            Response.AddPaginationHeader(posts.CurrentPage, posts.PageSize, posts.TotalCount, posts.TotalPages);

            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<QuestionAtHome>>> SearchQuestions(string name)
        {
            var posts = await _unitOfWork.QuestionRepository.SearchAsync(name);
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<SearchQuestionDto>>> GetAllQuestions()
        {
            var posts = await _unitOfWork.QuestionRepository.GetAll();
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("get-top-ten")]
        public async Task<ActionResult<IEnumerable<QuestionAtHome>>> GetTopTen()
        {
            var size = int.Parse(_config["Top-ten"].ToString());
            var posts = await _unitOfWork.QuestionRepository.GetTopTen(size);
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ServiceFilter(typeof(LogViewCountQuestion))]
        public async Task<ActionResult<QuestionDto>> GetQuestion(string id)
        {
            var post = await _unitOfWork.QuestionRepository.GetQuestionAsync(id);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(string id)
        {
            var q = await _unitOfWork.QuestionRepository.DeleteQuestion(id);
            if(q != null)
            {
                if (await _unitOfWork.Complete())
                    return Ok(_mapper.Map<QuestionDto>(q));//nếu có object trả về thì xóa thành công                    
                return BadRequest("Lỗi khi xóa");
            }
            return NoContent();
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddQuestion(CreateQuestionDto questionDto)
        {
            var q = new Question
            {
                Id = Guid.NewGuid(),
                Tittle = questionDto.Tittle,
                NoiDung = questionDto.NoiDung,
                UserId = User.GetUserId()
            };

            _unitOfWork.QuestionRepository.AddQuestion(q);
            //Them thanh cong thi them tags
            if (await _unitOfWork.Complete())
            {
                _unitOfWork.TagsLanguageRepository.Add(q.Id, questionDto.Tags);
                if (_unitOfWork.HasChanges())
                    await _unitOfWork.Complete();
                return Ok(_mapper.Map<QuestionDto>(q));
            }
            return BadRequest("Thêm câu hỏi thất bại");
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateQuestion(CreateQuestionDto questionDto)
        {
            await _unitOfWork.QuestionRepository.UpdateQuestion(questionDto);
            //update thanh cong thi update tags
            if (await _unitOfWork.Complete())
            {
                return NoContent();
            }
            return BadRequest("Sửa câu hỏi thất bại");
        }
    }
}
