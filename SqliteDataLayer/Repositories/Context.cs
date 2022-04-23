using System.IO;
using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SqliteDataLayer.Repositories
{
    public class Context : DbContext
    {
        public DbSet<Map> Maps{ get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Preset> Presets { get; set; }

        private string DbPath { get; }

        public Context()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var folder = Path.GetDirectoryName(path);
            DbPath = System.IO.Path.Join(folder, "Database/sqlite.db");
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options) => 
            options.UseSqlite($"Data Source={DbPath}");
    }
}
