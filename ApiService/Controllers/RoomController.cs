using System;
using System.Threading.Tasks;
using ApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController
    {
        private readonly RoomService _roomService;

        public RoomController()
        {
            _roomService = new RoomService();
        }

        [HttpGet]
        public async Task<Guid> GiveMeRoom(Guid playerId) => await _roomService.GetRoomForPlayer(playerId);
    }
}
