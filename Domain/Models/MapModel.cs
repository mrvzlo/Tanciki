using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;

namespace Domain.Models
{
    public class MapModel
    {
        public int Id { get; set; }
        public Guid RoomId { get; set; }
        public List<Tank> Tanks { get; set; }
        public PresetModel Preset { get; set; }

        public MapModel() { }

        public MapModel(Preset preset, RoomModel room)
        {
            Tanks = new List<Tank>();
            RoomId = room.Id;
            Preset = new PresetModel(preset);
        }

        public MapModel(Map map, Preset preset)
        {
            Id = map.Id;
            RoomId = Guid.Parse(map.RoomId);
            Tanks = JsonConvert.DeserializeObject<List<Tank>>(map.Tanks);
            Preset = new PresetModel(preset);
        }

        public Map ToEntity()
        {
            return new Map
            {
                Id = Id,
                PresetId = Preset.Id,
                RoomId = RoomId.ToString(),
                Tanks = JsonConvert.SerializeObject(Tanks)
            };
        }

        public ActionResult[] PerformAction(int tankId, ActionType action)
        {
            switch (action)
            {
                case ActionType.TurnLeft: return TurnLeft(tankId);
                case ActionType.TurnRight: return TurnRight(tankId);
                case ActionType.Move: return Move(tankId);
                default:
                    return new ActionResult[0];
            }
        }

        private ActionResult[] TurnLeft(int tankId)
        {
            if (!Tanks[tankId].CanMove()) return new ActionResult[0];
            var before = Tanks[tankId];
            Tanks[tankId].RotateLeft();
            var result = new ActionResult(before, Tanks[tankId]);
            return new[] { result };
        }

        private ActionResult[] TurnRight(int tankId)
        {
            if (!Tanks[tankId].CanMove()) return new ActionResult[0];
            var before = Tanks[tankId];
            Tanks[tankId].RotateRight();
            var result = new ActionResult(before, Tanks[tankId]);
            return new[] { result };
        }

        private ActionResult[] Move(int tankId)
        {
            if (!CanMove(Tanks[tankId])) return new ActionResult[0];
            var before = Tanks[tankId];
            Tanks[tankId].Move();
            var result = new ActionResult(before, Tanks[tankId]);
            return new[] { result };
        }

        private bool CanMove(Tank tank)
        {
            if (!tank.CanMove()) return false;
            var newPos = tank.Point.Move(tank.Direction);
            if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= Preset.Size || newPos.Y >= Preset.Size) return false;
            return Preset.Tiles[newPos.X][newPos.Y] == TileType.Air;
        }
    }
}
