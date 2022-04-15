using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models;
using ApiService.Models.Enums;

namespace ApiService.Services
{
    public class RoomService
    {
        private readonly FileDbService<Room> _roomDbService;

        public RoomService()
        {
            _roomDbService = new FileDbService<Room>("Rooms.json");
        }
        
        public async Task<Room> GetRoomForPlayer(Guid playerId)
        {
            var rooms = await _roomDbService.GetList();
            var firstNotFull = -1;
            for (var i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (room.Players.Contains(playerId) && room.CanReconnect())
                    return rooms[i];

                if (firstNotFull < 0 && room.CanJoin())
                    firstNotFull = i;
            }

            if (firstNotFull >= 0)
            {
                await AddToRoom(rooms, firstNotFull, playerId);
                return rooms[firstNotFull];
            }

            rooms.Add(Create());
            await AddToRoom(rooms, rooms.Count - 1, playerId);
            return rooms[^1];
        }

        private Room Create() => new Room(Guid.NewGuid());

        private async Task AddToRoom(List<Room> rooms, int index, Guid playerId)
        {
            rooms[index].Players.Add(playerId);
            if (rooms[index].Players.Count >= 2 && rooms[index].GameState == GameStateType.PreStart)
            {
                rooms[index].GameState = GameStateType.Running;
                await CreateMapForRoom(rooms[index].Id);
            }
            await _roomDbService.SaveList(rooms);
        }

        private async Task CreateMapForRoom(Guid id)
        {
            var mapService = new MapService(id);
            await mapService.CreateMap();
        }
    }
}
