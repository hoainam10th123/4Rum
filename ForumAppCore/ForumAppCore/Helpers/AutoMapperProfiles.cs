using AutoMapper;
using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();

            CreateMap<RegisterDto, AppUser>();

            CreateMap<TagsLanguage, TagsLanguageDto>();

            CreateMap<Question, SearchQuestionDto>();

            CreateMap<CommentParentDto, CommentParent>();

            CreateMap<CommentChildrentDto , ChildrentComment>();

            CreateMap<CommentParent, CommentParentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.AppUser.PhotoUrl));

            CreateMap<ChildrentComment, CommentChildrentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.AppUser.PhotoUrl));

            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.DisplayNameComment, opt => opt.MapFrom(src => src.UserComment.DisplayName))
                .ForMember(dest => dest.PhotoUrlComment, opt => opt.MapFrom(src => src.UserComment.PhotoUrl));

            CreateMap<QuestionTag, QuestionTagDto>()
                .ForMember(dest => dest.LaguageName, opt => opt.MapFrom(src => src.TagsLanguage.Name));

            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.AppUser.PhotoUrl));

            CreateMap<Question, QuestionAtHome>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.AppUser.PhotoUrl))
                .ForMember(dest => dest.CountCommentParents, opt => opt.MapFrom(src => src.CommentParents.Count));
        }
    }
}
