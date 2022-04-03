using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApiService.Models;
using Newtonsoft.Json;

namespace ApiService.Services
{
    public class RoomService
    {
        private const string FileName = "Database/Rooms.json";
        
        public async Task<Guid> GetRoomForPlayer(Guid playerId)
        {
            var rooms = await GetAll();
            var firstNotFull = -1;
            var alreadySet = -1;
            for (var i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (room.Players.Contains(playerId))
                {
                    alreadySet = i;
                    break;
                }

                if (room.Players.Count < 4 && firstNotFull < 0)
                    firstNotFull = i;
            }

            if (alreadySet >= 0) return rooms[alreadySet].Id;
            if (firstNotFull >= 0)
            {
                await AddToRoom(rooms, firstNotFull, playerId);
                return rooms[firstNotFull].Id;
            }

            rooms.Add(Create());
            await AddToRoom(rooms, rooms.Count - 1, playerId);
            return rooms[^1].Id;
        }

        private Room Create()
        {
            return new Room(Guid.NewGuid());
        }

        private async Task<List<Room>> GetAll()
        {
            var json = await File.ReadAllTextAsync(FileName);
            return JsonConvert.DeserializeObject<List<Room>>(json) ?? new List<Room>();
        }

        private async Task AddToRoom(List<Room> rooms, int id, Guid playerId)
        {
            rooms[id].Players.Add(playerId);
            await SaveAll(rooms);
        }

        private async Task SaveAll(List<Room> rooms)
        {
            var json = JsonConvert.SerializeObject(rooms);
            await File.WriteAllTextAsync(FileName, json);
        }
    }
}
