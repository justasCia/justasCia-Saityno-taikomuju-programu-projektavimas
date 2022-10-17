using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BallTalkAPI.Interfaces;
using BallTalkAPI.Entities;
using BallTalkAPI.Data.DTOs.Post;
using System.Net;

namespace BallTalkAPI.Controllers
{
    [Route("api/Topics/{topicId}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;

        private readonly IMapper _mapper;

        public PostsController(ITopicRepository topicRepository, IPostRepository postRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts(int topicId)
        {
            var topic = await GetTopic(topicId);

            var posts = (await _postRepository.GetPostsByTopicAsync(topicId))
                .OrderByDescending(post => post.Posted)
                .Select(post => _mapper.Map<PostDTO>(post));

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int topicId, int id)
        {
            var topic = await GetTopic(topicId);
            var post = topic.Posts.FirstOrDefault(post => post.Id == id);

            return post == null ? NotFound($"Post not found.") : Ok(_mapper.Map<PostDTO>(post));
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> PostPost(int topicId, AddPostDTO postDTO)
        {
            var topic = await GetTopic(topicId);

            var post = _mapper.Map<Post>(postDTO);
            post.Topic = topic;
            await _postRepository.AddPostAsync(post);

            return Created($"/api/Topics/{topicId}/Posts/{post.Id}", _mapper.Map<PostDTO>(post));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostDTO>> PutPost(int topicId, int id, PutPostDTO putPostDTO)
        {
            var topic = await GetTopic(topicId);

            var post = await _postRepository.GetPostAsync(id);

            if (post == null || post.TopicId != topic.Id)
            {
                return NotFound($"Post not found.");
            }

            _mapper.Map(putPostDTO, post);
            await _postRepository.PutPostAsync(post);

            return Ok(_mapper.Map<PostDTO>(post));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int topicId, int id)
        {
            var topic = await GetTopic(topicId);

            var post = await _postRepository.GetPostAsync(id);

            if (post == null || post.TopicId != topic.Id)
            {
                return NotFound($"Post not found.");
            }

            await _postRepository.DeletePostAsync(post);

            return NoContent();
        }

        [HttpPost("{id}/approve")]
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
