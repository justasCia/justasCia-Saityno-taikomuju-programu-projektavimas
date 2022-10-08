using Microsoft.AspNetCore.Mvc;
using BallTalkAPI.Entities;
using BallTalkAPI.Interfaces;
using AutoMapper;
using BallTalkAPI.Data.DTOs.Comment;

namespace BallTalkAPI.Controllers
{
    [Route("api/Topics/{topicName}/Posts/{postId:int}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;

        public CommentsController(ITopicRepository topicRepository, IPostRepository postRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments(string topicName, int postId)
        {
            var topic = await GetTopic(topicName);
            var post = await GetPost(postId, true);

            return post.Comments
                .Select(comment => _mapper.Map<CommentDTO>(comment))
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(string topicName, int postId, int id)
        {
            var topic = await GetTopic(topicName);
            var post = await GetPost(postId);

            var comment = post.Comments.FirstOrDefault(comment => comment.Id == id);

            return comment == null ? NotFound($"Comment with id {id} not found.") : Ok(_mapper.Map<CommentDTO>(comment));
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> PostComment(string topicName, int postId, AddOrUpdateCommentDTO addOrUpdateCommentDTO)
        {
            await GetTopic(topicName);
            var post = await GetPost(postId);

            var comment = _mapper.Map<Comment>(addOrUpdateCommentDTO);
            comment.Post = post;
            await _commentRepository.AddCommentAsync(comment);

            return Created($"/api/Topics/{topicName}/Posts/{postId}/Comments/{comment.Id}", _mapper.Map<CommentDTO>(comment));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentDTO>> PutComment(string topicName, int postId, int id, AddOrUpdateCommentDTO addOrUpdateCommentDTO)
        {
            await GetTopic(topicName);
            var post = await GetPost(postId, true);
            var comment = post.Comments.FirstOrDefault(comment => comment.Id == id);

            if (comment == null)
            {
                return NotFound($"Comment with id {id} not found.");
            }

            _mapper.Map(addOrUpdateCommentDTO, comment);
            await _commentRepository.PutCommentAsync(comment);

            return Ok(_mapper.Map<CommentDTO>(comment));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(string topicName, int postId, int id)
        {
            await GetTopic(topicName);
            var post = await GetPost(postId, true);
            var comment = post.Comments.FirstOrDefault(comment => comment.Id == id);

            if (comment == null)
            {
                return NotFound($"Comment with id {id} not found.");
            }

            await _commentRepository.DeleteCommentAsync(comment);

            return NoContent();
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

        private async Task<Post> GetPost(int id, bool includeComments = false)
        {
            var post = await _postRepository.GetPostAsync(id, includeComments);

            if (post == null)
            {
                throw new BadHttpRequestException($"Post with id {id} not found.", StatusCodes.Status404NotFound);
            }

            return post;
        }
    }
}
