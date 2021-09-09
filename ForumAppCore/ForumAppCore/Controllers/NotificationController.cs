using ForumAppCore.Extensions;
using ForumAppCore.Helpers;
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
    public class NotificationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-pagination")]
        public async Task<ActionResult> GetPagination([FromQuery] NotificationParams notifiParams)
        {
            notifiParams.CurrentUserId = User.GetUserId();
            var model = await _unitOfWork.NotificationRepository.GetAll(notifiParams);
            Response.AddPaginationHeader(model.CurrentPage, model.PageSize, model.TotalCount, model.TotalPages);
            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetUnread()
        {
            var model = await _unitOfWork.NotificationRepository.GetUnRead(User.GetUserId());
            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult> Update()
        {
            await _unitOfWork.NotificationRepository.SetRead(User.GetUserId());

            if (_unitOfWork.HasChanges())
            {
                if(!await _unitOfWork.Complete())
                {
                    return BadRequest("Lỗi khi cập nhật notification");
                }             
            }       

            return NoContent();
        }
    }
}
