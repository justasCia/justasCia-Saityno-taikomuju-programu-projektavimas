using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BallTalkAPI.Interfaces;
using BallTalkAPI.Entities;
using BallTalkAPI.Data.DTOs.Post;

namespace BallTalkAPI.Controllers
{
    [Route("api/Topics/{topicName:string}/[controller]")]
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
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts(string name)
        {
            var topic = await GetTopic(name);

            return topic.Posts
                .Select(post => _mapper.Map<PostDTO>(post))
                .ToList();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(string topicName, int id)
        {
            var topic = await GetTopic(topicName);
            var post = topic.Posts.FirstOrDefault(post => post.Id == id);

            return post == null ? NotFound() : Ok(_mapper.Map<PostDTO>(post));
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
                return NotFound();
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
                return NotFound();
            }

            await _postRepository.DeletePostAsync(post);

            return NoContent();
        }

        private async Task<Topic> GetTopic(string topicName)
        {
            var topic = await _topicRepository.GetTopicByNameAsync(topicName, true);

            if (topic == null)
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            return topic;
        }
    }
}
