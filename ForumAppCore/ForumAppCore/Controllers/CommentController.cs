using AutoMapper;
using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using ForumAppCore.Extensions;
using ForumAppCore.Interfaces;
using ForumAppCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Controllers
{
    [Authorize]
    public class CommentController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        IHubContext<PresenceHub> _presenceHub;
        PresenceTracker _presenceTracker;

        public CommentController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<PresenceHub> presenceHub, PresenceTracker presenceTracker)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _presenceHub = presenceHub;
            _presenceTracker = presenceTracker;
        }

        [HttpGet("get-parent/{id}")]
        public async Task<ActionResult> GetCommentParent(string id)
        {
            var model = await _unitOfWork.CommentRepository.GetCommentParent(Guid.Parse(id));
            return Ok(model);
        }

        [HttpGet("get-childrent/{id}")]
        public async Task<ActionResult> GetCommentChildrent(int id)
        {
            var model = await _unitOfWork.CommentRepository.GetCommentChildrent(id);
            return Ok(model);
        }

        [HttpPost("add-parent")]
        public async Task<ActionResult> AddCommentParent(CreateCommentDto commentDto)
        {
            var idQuestion = Guid.Parse(commentDto.QuestionId);
            var parentId = Guid.NewGuid();
            var comment = new CommentParent
            {
                Id = parentId,
                NoiDung = commentDto.NoiDung,
                UserId = User.GetUserId(),
                QuestionId = idQuestion
            };

            _unitOfWork.CommentRepository.AddCommentParent(comment);

            if (await _unitOfWork.Complete())
            {
                var rootUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(commentDto.UserCommentTo);
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(User.GetUserId());
                var notification = new Notification 
                { 
                    Id = Guid.NewGuid(),
                    UserId = rootUser.Id,
                    QuestionId = idQuestion,
                    UserCommentId = User.GetUserId(),
                    NoiDung = user.DisplayName+ " đã trả lời câu hỏi của bạn",
                    IsRead = false,
                    CommentParentId = parentId
                };
                _unitOfWork.NotificationRepository.Add(notification);

                if(await _unitOfWork.Complete())
                {
                    var connections = await _presenceTracker.GetConnectionsForUser(commentDto.UserCommentTo);
                    if (connections != null)
                        await _presenceHub.Clients.Clients(connections).SendAsync("Notification", 
                            new { username = user.UserName, displayname = user.DisplayName });
                }

                return Ok(await _unitOfWork.CommentRepository.GetCommentParent(comment.Id));
            }
            return BadRequest("Thêm câu trả lời thất bại");
        }

        [HttpPost("add-childrent")]
        public async Task<ActionResult> AddCommentChildrent(CreateCommentDto commentDto)
        {
            if (ModelState.IsValid)
            {
                var comment = new ChildrentComment
                {
                    NoiDung = commentDto.NoiDung,
                    UserId = User.GetUserId(),
                    ParentId = Guid.Parse(commentDto.ParentId)
                };

                _unitOfWork.CommentRepository.AddCommentChidren(comment);

                if (await _unitOfWork.Complete())
                {
                    var idQuestion = Guid.Parse(commentDto.QuestionId);
                    var rootUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(commentDto.UserCommentTo);
                    var user = await _unitOfWork.UserRepository.GetUserByIdAsync(User.GetUserId());
                    var notification = new Notification
                    {
                        Id = Guid.NewGuid(),
                        UserId = rootUser.Id,
                        QuestionId = idQuestion,
                        UserCommentId = User.GetUserId(),
                        NoiDung = user.DisplayName + " đã bình luận câu trả lời của bạn",
                        IsRead = false,
                        CommentParentId = Guid.Parse(commentDto.ParentId),
                        CommentChildrentId = comment.Id
                    };
                    _unitOfWork.NotificationRepository.Add(notification);

                    if (await _unitOfWork.Complete())
                    {
                        var connections = await _presenceTracker.GetConnectionsForUser(commentDto.UserCommentTo);
                        if (connections != null)
                            await _presenceHub.Clients.Clients(connections).SendAsync("Notification",
                                new { username = user.UserName, displayname = user.DisplayName });
                    }
                    return Ok(await _unitOfWork.CommentRepository.GetCommentChildrent(comment.Id));
                }
                return BadRequest("Thêm bình luận thất bại");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
            
        }

        [HttpDelete("delete-parent/{id}")]
        public async Task<ActionResult> DeleteParent(string id)
        {
            var q = await _unitOfWork.CommentRepository.DeleteCommentParent(id);
            if (q != null)
            {
                if (await _unitOfWork.Complete())
                    return Ok(_mapper.Map<CommentParentDto>(q));                
                return BadRequest("Lỗi khi xóa");
            }
            return NoContent();
        }

        [HttpDelete("delete-childrent/{id}")]
        public async Task<ActionResult> DeleteChildrent(int id)
        {
            var q = await _unitOfWork.CommentRepository.DeleteCommentChildrent(id);
            if (q != null)
            {
                if (await _unitOfWork.Complete())
                    return Ok(_mapper.Map<CommentChildrentDto>(q));
                return BadRequest("Lỗi khi xóa");
            }
            return NoContent();
        }

        [HttpPut("update-parent")]
        public async Task<ActionResult<MemberDto>> UpdateParentComment(CommentParentDto commentDto)
        {
            var appUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(commentDto.UserName);
            var t = _mapper.Map<CommentParent>(commentDto);
            t.UserId = appUser.Id;

            _unitOfWork.CommentRepository.UpdateParent(t);

            if (await _unitOfWork.Complete()) return Ok();
            return BadRequest("Lỗi khi cập nhật câu trả lời");
        }

        [HttpPut("update-childrent")]
        public async Task<ActionResult<MemberDto>> UpdateChildComment(CommentChildrentDto commentDto)
        {
            var appUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(commentDto.UserName);
            var t = _mapper.Map<ChildrentComment>(commentDto);
            t.UserId = appUser.Id;

            _unitOfWork.CommentRepository.UpdateChildrent(t);

            if (await _unitOfWork.Complete()) return NoContent();
            return BadRequest("Lỗi khi cập nhật bình luận");
        }
    }
}
