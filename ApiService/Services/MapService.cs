using System;
using System.Collections.Generic;
using Domain.Models;
using SqliteDataLayer.Repositories;

namespace ApiService.Services
{
    public class MapService
    {
        private readonly MapRepository _mapRepository;
        private readonly PresetRepository _presetRepository;

        public MapService()
        {
            _mapRepository = new MapRepository();
            _presetRepository = new PresetRepository();
        }

        public void CreateMap(RoomModel room)
        {
            var preset = _presetRepository.GetFirst();
            var gameMap = new MapModel(preset, room);
            var corners = GetCorners(room.Players.Count, preset.Size);
            for (var i = 0; i < room.Players.Count; i++) 
                gameMap.Tanks.Add(new Tank(room.Players[i], corners[i]));

            var entity = gameMap.ToEntity();
            _mapRepository.Create(entity);
        }

        public MapModel Get(Guid id)
        {
            var map = _mapRepository.GetByRoom(id);
            var preset = _presetRepository.Get(map.PresetId);
            return new MapModel(map, preset);
        }

        private List<Point> GetCorners(int count, int max)
        {
            return new List<Point>
            {
                new Point(0, 0),
                new Point(0, max-1),
                new Point(max-1, 0),
                new Point(max-1, max-1)
            };
        }
    }
}
