using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BallTalkAPI.Interfaces;
using BallTalkAPI.Entities;
using BallTalkAPI.Data.DTOs.Post;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using BallTalkAPI.Auth.Entities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BallTalkAPI.Auth;

namespace BallTalkAPI.Controllers
{
    [Route("api/Topics/{topicId}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;

        private readonly IAuthorizationService _authorizationService;

        private readonly IMapper _mapper;

        public PostsController(ITopicRepository topicRepository, IPostRepository postRepository, IAuthorizationService authorizationService, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts(int topicId)
        {
            var topic = await GetTopic(topicId);

            var posts = (await _postRepository.GetPostsByTopicAsync(topicId))
                .OrderByDescending(post => post.Posted)
                .Select(post => _mapper.Map<PostDTO>(post));

            return Ok(posts);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<PostDTO>> GetPost(int topicId, int id)
        {
            var topic = await GetTopic(topicId);
            var post = topic.Posts.FirstOrDefault(post => post.Id == id);

            return post == null ? NotFound($"Post not found.") : Ok(_mapper.Map<PostDTO>(post));
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<PostDTO>> PostPost(int topicId, AddPostDTO postDTO)
        {
            var topic = await GetTopic(topicId);

            var post = _mapper.Map<Post>(postDTO);
            post.Topic = topic;
            post.UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            await _postRepository.AddPostAsync(post);

            return Created($"/api/Topics/{topicId}/Posts/{post.Id}", _mapper.Map<PostDTO>(post));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<PostDTO>> PutPost(int topicId, int id, PutPostDTO putPostDTO)
        {
            var topic = await GetTopic(topicId);

            var post = await _postRepository.GetPostAsync(id);

            if (post == null || post.TopicId != topic.Id)
            {
                return NotFound($"Post not found.");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.ResourceOwner);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(putPostDTO, post);
            await _postRepository.PutPostAsync(post);

            return Ok(_mapper.Map<PostDTO>(post));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> DeletePost(int topicId, int id)
        {
            var topic = await GetTopic(topicId);

            var post = await _postRepository.GetPostAsync(id);

            if (post == null || post.TopicId != topic.Id)
            {
                return NotFound($"Post not found.");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.ResourceOwner);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _postRepository.DeletePostAsync(post);

            return NoContent();
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<PostDTO>> ApprovePost(int topicId, int id)
        {
            var topic = await GetTopic(topicId);

            var post = await _postRepository.GetPostAsync(id);

            if (post == null || post.TopicId != topic.Id)
            {
                return NotFound($"Post not found.");
            }

            if (post.Approved)
            {
                return Conflict($"Post already approved.");
            }

            post.Approved = true;
            await _postRepository.PutPostAsync(post);

            return Ok(_mapper.Map<PostDTO>(post));
        }

        private async Task<Topic> GetTopic(int topicId)
        {
            var topic = await _topicRepository.GetTopicAsync(topicId);

            if (topic == null)
            {
                throw new BadHttpRequestException($"Topic not found.", StatusCodes.Status404NotFound);
            }

            return topic;
        }
    }
}
