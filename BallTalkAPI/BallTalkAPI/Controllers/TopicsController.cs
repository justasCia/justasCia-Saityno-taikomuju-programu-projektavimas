using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BallTalkAPI.Entities;
using BallTalkAPI.Interfaces;
using AutoMapper;
using BallTalkAPI.Data.DTOs;

namespace BallTalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicsController(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetTopics()
        {
            return (await _topicRepository.GetTopicsAsync())
                .Select(topic => _mapper.Map<TopicDTO>(topic))
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDTO>> GetTopic(int id)
        {
            var topic = await _topicRepository.GetTopicAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            return _mapper.Map<TopicDTO>(topic);
        }

        [HttpPost]
        public async Task<ActionResult<TopicDTO>> PostTopic(TopicDTO topicDTO)
        {
            var topic = (await _topicRepository.GetTopicsAsync()).FirstOrDefault(topic => topic.Name == topicDTO.Name);

            if (topic != null)
            {
                return Conflict();
            }

            await _topicRepository.AddTopicAsync(_mapper.Map<Topic>(topicDTO));

            return Created($"/api/topics/{topicDTO.Name}", topicDTO);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult<TopicDTO>> PutTopic(string name, TopicDTO topicDTO)
        {
            if (await _topicRepository.GetTopicByNameAsync(topicDTO.Name) != null)
            {
                return BadRequest();
            }

            var topic = await _topicRepository.GetTopicByNameAsync(name);

            if (topic == null)
            {
                return NotFound();
            }

            _mapper.Map(topicDTO, topic);
            await _topicRepository.PutTopicAsync(topic);

            return Ok(_mapper.Map<TopicDTO>(topic));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _topicRepository.GetTopicAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            await _topicRepository.DeleteTopicAsync(topic);

            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteTopic(string name)
        {
            var topic = await _topicRepository.GetTopicByNameAsync(name);

            if (topic == null)
            {
                return NotFound();
            }

            await _topicRepository.DeleteTopicAsync(topic);

            return NoContent();
        }
    }
}
