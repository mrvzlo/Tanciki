using System;
using System.Linq;
using Domain.Enums;
using Domain.Models;
using SqliteDataLayer.Repositories;

namespace ApiService.Services
{
    public class RoomService
    {
        private readonly RoomRepository _roomRepository;
        private readonly MapService _mapService;

        public RoomService()
        {
            _roomRepository = new RoomRepository();
            _mapService = new MapService();
        }

        public RoomModel GetRoomForPlayer(Guid playerId)
        {
            var rooms = _roomRepository.GetList().Select(x => new RoomModel(x));
            var alreadyAdded = rooms.FirstOrDefault(x => x.Players.Contains(playerId) && x.CanReconnect());
            if (alreadyAdded != null) return alreadyAdded;
            var firstNotFull = rooms.FirstOrDefault(x => x.CanJoin());
            if (firstNotFull != null)
            {
                AddToRoom(firstNotFull, playerId);
                return firstNotFull;
            }

            var room = new RoomModel(Guid.NewGuid());
            CreateRoom(room, playerId);
            return room;
        }

        private void CreateRoom(RoomModel room, Guid playerId)
        {
            room.Players.Add(playerId);
            CheckStart(room);
            var entity = room.ToEntity();
            _roomRepository.Create(entity);
        }

        private void AddToRoom(RoomModel room, Guid playerId)
        {
            room.Players.Add(playerId);
            CheckStart(room);
            var entity = room.ToEntity();
            _roomRepository.Update(entity);
        }

        private void CheckStart(RoomModel room)
        {
            if (!room.CanStart()) return;
            room.GameState = GameStateType.Running;
            _mapService.CreateMap(room);

        }
    }
}
