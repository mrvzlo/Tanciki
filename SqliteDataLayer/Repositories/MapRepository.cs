using System;
using System.Linq;
using Domain.Entities;

namespace SqliteDataLayer.Repositories
{
    public class MapRepository : BaseRepository<Map>
    {
        public Map GetByRoom(Guid id)
        {
            using var db = new Context();
            return db.Maps.FirstOrDefault(x => x.RoomId == id.ToString());
        }
    }
}
