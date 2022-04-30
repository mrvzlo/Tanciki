using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain.Enums;
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
                gameMap.Tanks.Add(new Tank(room.Players[i], corners[i], WalkingObjectState.Alive));

            var entity = gameMap.ToEntity();
            _mapRepository.Create(entity);
        }

        public List<ActionResult> PerformAction(Guid playerId, Guid roomId, ActionType action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var map = GetMapByRoomId(roomId);
            var tank = map.Tanks.FindIndex(x => x.PlayerId == playerId && x.State == WalkingObjectState.Alive);
            if (tank < 0) return new List<ActionResult>();
            var result = map.PerformAction(tank, action);
            Console.WriteLine("Before DB save {0} ms", stopwatch.ElapsedMilliseconds);
            _mapRepository.Update(map.ToEntity());
            stopwatch.Stop();
            Console.WriteLine("After DB save {0} ms", stopwatch.ElapsedMilliseconds);
            return result.ToList();
        }

        public MapModel GetMapByRoomId(Guid id)
        {
            var map = _mapRepository.GetByRoom(id);
            var preset = _presetRepository.Get(map.PresetId);
            return new MapModel(map, preset);
        }

        private List<Point> GetCorners(int count, int max)
        {
            return new List<Point> { new (0, 0), new(max - 1, max - 1), new (0, max-1), new (max-1, 0) }
                .Take(count).ToList();
        }
    }
}
