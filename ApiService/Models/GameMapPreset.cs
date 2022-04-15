using ApiService.Models.Enums;

namespace ApiService.Models
{
    public class GameMapPreset
    {
        public TileType[,] Tiles { get; set; }
        public int Size { get; set; }
    }
}
