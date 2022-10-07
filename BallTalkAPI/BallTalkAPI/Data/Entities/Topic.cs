﻿using System.ComponentModel.DataAnnotations;

namespace BallTalkAPI.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
