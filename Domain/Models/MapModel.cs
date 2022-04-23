using System;
using System.Collections.Generic;
using Domain.Entities;
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
    }
}
