using Domain.Enums;

namespace Domain.Models
{
    public class Tile : Point
    {
        public TileType TileType { get; set; }
        public TokenType Occupied { get; set; }
        public int OccupiedId { get; set; }

        public Tile(){}

        public Tile(int x, int y, TileType tileType) : base(x, y)
        {
            TileType = tileType;
        }
    }
}
