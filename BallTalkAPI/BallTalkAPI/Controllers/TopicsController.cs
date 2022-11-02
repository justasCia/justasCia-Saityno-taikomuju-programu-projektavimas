using Microsoft.AspNetCore.Mvc;
using BallTalkAPI.Entities;
using BallTalkAPI.Interfaces;
using AutoMapper;
using BallTalkAPI.Data.DTOs.Topic;
using BallTalkAPI.Auth.Entities;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetTopics()
        {
            return (await _topicRepository.GetTopicsAsync())
                .Select(topic => _mapper.Map<TopicDTO>(topic))
                .ToList();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<TopicDTO>> GetTopic(int id)
        {
            var topic = await _topicRepository.GetTopicAsync(id);

            if (topic == null)
            {
                return NotFound($"Topic with id {id} not found.");
            }

            return _mapper.Map<TopicDTO>(topic);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<TopicDTO>> PostTopic(AddOrUpdateTopicDTO topicDTO)
        {
            var topic = (await _topicRepository.GetTopicsAsync()).FirstOrDefault(topic => topic.Name == topicDTO.Name);

            if (topic != null)
            {
                return Conflict($"Topic with name {topicDTO.Name} already exists.");
            }

            topic = await _topicRepository.AddTopicAsync(_mapper.Map<Topic>(topicDTO));

            return Created($"/api/topics/{topicDTO.Name}", _mapper.Map<TopicDTO>(topic));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<TopicDTO>> PutTopic(int id, AddOrUpdateTopicDTO topicDTO)
        {
            if (await _topicRepository.GetTopicByNameAsync(topicDTO.Name) != null)
            {
                return Conflict($"Topic with name {topicDTO.Name} already exists.");
            }

            var topic = await _topicRepository.GetTopicAsync(id);

            if (topic == null)
            {
                return NotFound($"Topic with id {id} not found.");
            }

            _mapper.Map(topicDTO, topic);
            await _topicRepository.PutTopicAsync(topic);

            return Ok(_mapper.Map<TopicDTO>(topic));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _topicRepository.GetTopicAsync(id);

            if (topic == null)
            {
                return NotFound($"Topic with id {id} not found.");
            }

            await _topicRepository.DeleteTopicAsync(topic);

            return NoContent();
        }
    }
}
