using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Models
{
    public class RoomModel
    {
        public Guid Id { get; set; }
        public List<Guid> Players { get; set; }
        public GameStateType GameState { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public RoomModel(){}

        public RoomModel(Guid id)
        {
            Id = id;
            DateCreated = DateTime.UtcNow;
            GameState = GameStateType.PreStart;
            Players = new List<Guid>();
        }

        public RoomModel(Room room)
        {
            Id = Guid.Parse(room.Id);
            Players = room.Players.Split(',').Select(Guid.Parse).ToList();
            DateCreated = DateTime.Parse(room.DateCreated);
            GameState = room.GameState;
        }

        public Room ToEntity()
        {
            return new Room
            {
                DateCreated = DateCreated.ToString(),
                GameState = GameState,
                Id = Id.ToString(),
                Players = string.Join(",", Players.Select(x => x.ToString()))
            };
        }

        public bool CanJoin() => GameState == GameStateType.PreStart && Players.Count < Consts.MaxPlayersInRoom;
        public bool CanReconnect() => GameState != GameStateType.End;
        public bool CanPlay() => GameState == GameStateType.Running;
        public bool CanStart() => Players.Count >= 2 && GameState == GameStateType.PreStart;
    }
}
