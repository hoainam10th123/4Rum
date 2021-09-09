using ForumAppCore.DTOs;
using ForumAppCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Controllers
{
    [Authorize]
    public class TagLaguageController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagLaguageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagsLanguageDto>>> GetAll()
        {
            var posts = await _unitOfWork.TagsLanguageRepository.GetTagsLanguageAsync();
            return Ok(posts);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<TagsLanguageDto>>> Search(string name)
        {
            var posts = await _unitOfWork.TagsLanguageRepository.SearchAsync(name);
            return Ok(posts);
        }
    }
}
