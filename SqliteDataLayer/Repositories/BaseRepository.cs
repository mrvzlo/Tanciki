using System.Collections.Generic;
using System.Linq;

namespace SqliteDataLayer.Repositories
{
    public class BaseRepository<TEntity> where TEntity: class
    {
        public TEntity GetFirst()
        {
            using var db = new Context();
            return db.Set<TEntity>().First();
        }

        public TEntity Get(int id)
        {
            using var db = new Context();
            return db.Set<TEntity>().Find(id);
        }
        
        public List<TEntity> GetList()
        {
            using var db = new Context();
            return db.Set<TEntity>().ToList();
        }

        public void Create(TEntity entity)
        {
            using var db = new Context();
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            using var db = new Context();
            db.Set<TEntity>().Update(entity);
            db.SaveChanges();
        }
    }
}
