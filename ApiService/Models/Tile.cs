using ApiService.Models.Enums;

namespace ApiService.Models
{
    public class Tile : Point
    {
        public TileType TileType { get; }
        public TokenType Occupied { get; set; }
        public int OccupiedId { get; set; }

        public Tile(int x, int y, TileType tileType) : base(x, y)
        {
            TileType = tileType;
        }
    }
}
