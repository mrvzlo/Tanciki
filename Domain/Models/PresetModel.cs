using System.Linq;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Models
{
    public class PresetModel
    {
        public int Id { get; set; }
        public TileType[][] Tiles { get; set; }
        public int Size { get; set; }

        public PresetModel() { }

        public PresetModel(Preset entity)
        {
            Id = entity.Id;
            Size = entity.Size;
            Tiles = new TileType[Size][];
            for (var i = 0; i < Size; i++) 
                Tiles[i] = new TileType[Size];
            var parsed = entity.Tiles.Split(',').Select(int.Parse).ToList();
            for (var i = 0; i < parsed.Count; i++)
                Tiles[i % Size][i / Size] = (TileType)parsed[i];
        }
    }
}
