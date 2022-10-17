using Microsoft.AspNetCore.Mvc;
using BallTalkAPI.Entities;
using BallTalkAPI.Interfaces;
using AutoMapper;
using BallTalkAPI.Data.DTOs.Comment;

namespace BallTalkAPI.Controllers
{
    [Route("api/Topics/{topicId}/Posts/{postId:int}/[controller]")]
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
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments(int topicId, int postId)
        {
            var (topic, post) = await GetTopicAndPost(topicId, postId);

            var comments = (await _commentRepository.GetCommentsByPostAsync(postId))
                .OrderByDescending(comment => comment.Posted)
                .Select(comment => _mapper.Map<CommentDTO>(comment));

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int topicId, int postId, int id)
        {
            var (topic, post) = await GetTopicAndPost(topicId, postId);

            var comment = await _commentRepository.GetCommentAsync(id);

            if (comment == null || comment.PostId != post.Id)
            {
                return NotFound($"Comment not found.");
            }


            return Ok(_mapper.Map<CommentDTO>(comment));
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> PostComment(int topicId, int postId, AddOrUpdateCommentDTO addOrUpdateCommentDTO)
        {
            var (topic, post) = await GetTopicAndPost(topicId, postId);

            var comment = _mapper.Map<Comment>(addOrUpdateCommentDTO);
            comment.Post = post;
            await _commentRepository.AddCommentAsync(comment);

            return Created($"/api/Topics/{topicId}/Posts/{postId}/Comments/{comment.Id}", _mapper.Map<CommentDTO>(comment));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentDTO>> PutComment(int topicId, int postId, int id, AddOrUpdateCommentDTO addOrUpdateCommentDTO)
        {
            var (topic, post) = await GetTopicAndPost(topicId, postId);

            var comment = await _commentRepository.GetCommentAsync(id);

            if (comment == null || comment.PostId != post.Id)
            {
                return NotFound($"Comment not found.");
            }

            _mapper.Map(addOrUpdateCommentDTO, comment);
            await _commentRepository.PutCommentAsync(comment);

            return Ok(_mapper.Map<CommentDTO>(comment));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int topicId, int postId, int id)
        {
            var (topic, post) = await GetTopicAndPost(topicId, postId);

            var comment = await _commentRepository.GetCommentAsync(id);

            if (comment == null || comment.PostId != post.Id)
            {
                return NotFound($"Comment not found.");
            }

            await _commentRepository.DeleteCommentAsync(comment);

            return NoContent();
        }

        private async Task<(Topic, Post)> GetTopicAndPost(int topicId, int postId)
        {
            var topic = await _topicRepository.GetTopicAsync(topicId);

            if (topic == null)
            {
                throw new BadHttpRequestException($"Topic not found.", StatusCodes.Status404NotFound);
            }

            var post = await _postRepository.GetPostAsync(postId);

            if (post == null || post.TopicId != topic.Id)
            {
                throw new BadHttpRequestException($"Post not found.", StatusCodes.Status404NotFound);
            }

            return (topic, post);
        }
    }
}
