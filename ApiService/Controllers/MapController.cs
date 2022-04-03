using Microsoft.AspNetCore.Mvc;
using ApiService.Models;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapController : ControllerBase
    {
        [HttpGet]
        public GameMap Get()
        {
            return new GameMap(8);
        }
    }
}
