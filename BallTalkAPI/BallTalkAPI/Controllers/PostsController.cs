using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BallTalkAPI.Interfaces;
using BallTalkAPI.Entities;
using BallTalkAPI.Data.DTOs.Post;
using System.Net;

namespace BallTalkAPI.Controllers
{
    [Route("api/Topics/{topicName}/[controller]")]
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
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts(string topicName)
        {
            var topic = await GetTopic(topicName);

            return topic.Posts
                .OrderByDescending(post => post.Posted)
                .Select(post => _mapper.Map<PostDTO>(post))
                .ToList();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(string topicName, int id)
        {
            var topic = await GetTopic(topicName);
            var post = topic.Posts.FirstOrDefault(post => post.Id == id);

            return post == null ? NotFound($"Post with id {id} not found.") : Ok(_mapper.Map<PostDTO>(post));
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> PostPost(string topicName, AddPostDTO postDTO)
        {
            var topic = await GetTopic(topicName);

            var post = _mapper.Map<Post>(postDTO);
            post.Topic = topic;
            await _postRepository.AddPostAsync(post);

            return Created($"/api/Topics/{topicName}/Posts/{post.Id}", _mapper.Map<PostDTO>(post));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostDTO>> PutPost(string topicName, int id, PutPostDTO putPostDTO)
        {
            var topic = await GetTopic(topicName);
            var post = topic.Posts.FirstOrDefault(post => post.Id == id);

            if (post == null)
            {
                return NotFound($"Post with id {id} not found.");
            }

            _mapper.Map(putPostDTO, post);
            await _postRepository.PutPostAsync(post);

            return Ok(_mapper.Map<PostDTO>(post));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string topicName, int id)
        {
            var topic = await GetTopic(topicName);
            var post = topic.Posts.FirstOrDefault(post => post.Id == id);

            if (post == null)
            {
                return NotFound($"Post with id {id} not found.");
            }

            await _postRepository.DeletePostAsync(post);

            return NoContent();
        }

        [HttpPost("{id}/approve")]
        public async Task<ActionResult<PostDTO>> ApprovePost(string topicName, int id)
        {
            var topic = await GetTopic(topicName);
            var post = topic.Posts.FirstOrDefault(post => post.Id == id);

            if (post == null)
            {
                return NotFound($"Post with id {id} not found.");
            }

            if (post.Approved)
            {
                return Conflict($"Post with id {id} already approved.");
            }

            post.Approved = true;
            await _postRepository.PutPostAsync(post);

            return Ok(_mapper.Map<PostDTO>(post));
        }

        private async Task<Topic> GetTopic(string topicName)
        {
            var topic = await _topicRepository.GetTopicByNameAsync(topicName, true);

            if (topic == null)
            {
                throw new BadHttpRequestException($"Topic {topicName} not found.", StatusCodes.Status404NotFound);
            }

            return topic;
        }
    }
}
