using System;
using System.Collections.Generic;
using ApiService.Models.Enums;

namespace ApiService.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public List<Guid> Players { get; set; }
        public GameStateType GameState { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Room(Guid id)
        {
            Id = id;
            DateCreated = DateTime.UtcNow;
            Players = new List<Guid>();
        }
    }
}
