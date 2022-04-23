using System;
using System.Threading.Tasks;
using ApiService.Services;
using Domain.Enums;
using Domain.Models;
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

        public void ConnectPlayer(Guid playerId)
        {
            var room = _roomService.GetRoomForPlayer(playerId);
            UpdateRoom(room);
        }

        public async Task GetMap(Guid playerId, Guid roomId)
        {
            //todo validate
            _mapService = new MapService();
            var map = _mapService.Get(roomId);
            await Clients.Caller.SendCoreAsync("mapUpdate", new[] { map });
        }

        public async Task Action(Guid playerId, Guid roomId, ActionType action)
        {

        }

        private void UpdateRoom(RoomModel room)
        {
            Clients.All.SendCoreAsync("roomUpdate", new[] { room });
        }
    }
}
