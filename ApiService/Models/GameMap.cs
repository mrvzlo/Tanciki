using ApiService.Models.Enums;

namespace ApiService.Models
{
    public class GameMap
    {
        public int MapSize { get; }
        public Tile[,] Tiles { get; }

        public GameMap(int size)
        {
            MapSize = size;
            Tiles = new Tile[size, size];
        }

        public void SetTiles(TileType[,] preset)
        {
            for (var x = 0; x < MapSize; x++)
                for (var y = 0; y < MapSize; y++)
                    Tiles[x, y] = new Tile(x, y, preset[x, y]);
        }
    }
}
