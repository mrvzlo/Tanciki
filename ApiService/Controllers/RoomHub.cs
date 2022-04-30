using System;
using System.Diagnostics;
using System.Linq;
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
            var map = _mapService.GetMapByRoomId(roomId);
            await Clients.Caller.SendCoreAsync("mapUpdate", new[] { map });
        }

        public async Task PerformAction(Guid playerId, Guid roomId, ActionType action)
        {
            _mapService = new MapService();
            var results = _mapService.PerformAction(playerId, roomId, action);
            await Clients.Caller.SendCoreAsync("resolveAction", new[] { results });
        }

        private void UpdateRoom(RoomModel room)
        {
            Clients.All.SendCoreAsync("roomUpdate", new[] { room });
        }
    }
}
