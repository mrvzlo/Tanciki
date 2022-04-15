using System;
using System.Threading.Tasks;
using ApiService.Models;
using ApiService.Services;
using Microsoft.AspNetCore.SignalR;

namespace ApiService.Controllers
{
    public class RoomHub : Hub
    {
        private readonly RoomService _roomService;
        private MapService _mapService;

        public RoomHub()
        {
            _roomService = new RoomService();
        }

        public void StartConnection()
        {
            Console.WriteLine("Connected");
        }

        public async Task ConnectPlayer(Guid playerId)
        {
            var room = await _roomService.GetRoomForPlayer(playerId);
            UpdateRoom(room);
        }

        public async Task GetMap(Guid playerId, Guid roomId)
        {
            //todo validate
            _mapService = new MapService(roomId);
            var map = await _mapService.Get();
            await Clients.Caller.SendCoreAsync("mapUpdate", new[] { map });
        }

        public void UpdateRoom(Room room)
        {
            Clients.All.SendCoreAsync("roomUpdate", new[] { room });
        }
    }
}
