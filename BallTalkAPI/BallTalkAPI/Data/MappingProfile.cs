using AutoMapper;
using BallTalkAPI.Data.DTOs;
using BallTalkAPI.Data.DTOs.Comment;
using BallTalkAPI.Data.DTOs.Post;
using BallTalkAPI.Entities;

namespace BallTalkAPI.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping Topic
            CreateMap<Topic, TopicDTO>();
            CreateMap<TopicDTO, Topic>();

            // Mapping Post
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
            CreateMap<AddPostDTO, Post>();
            CreateMap<PutPostDTO, Post>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping Comment
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<AddOrUpdateCommentDTO, Comment>();
        }
    }
}
