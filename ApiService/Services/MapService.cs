using System;
using System.Threading.Tasks;
using ApiService.Models;
using ApiService.Models.Enums;

namespace ApiService.Services
{
    public class MapService
    {
        private readonly FileDbService<GameMap> _mapDbService;
        private readonly FileDbService<GameMapPreset> _presetDbService;

        public MapService(Guid id)
        {
            var filename = "Map" + id + ".json";
            _mapDbService = new FileDbService<GameMap>(filename);
            _presetDbService = new FileDbService<GameMapPreset>("MapPreset1.json");
        }

        public async Task CreateMap()
        {
            var preset = await _presetDbService.Get();
            var gameMap = new GameMap(preset.Size);
            gameMap.SetTiles(preset.Tiles);
            await _mapDbService.Save(gameMap);
        }

        public Task<GameMap> Get() => _mapDbService.Get();
    }
}
