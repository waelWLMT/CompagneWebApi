using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyDataBaseContext context;
        private readonly DbSet<T> _entities;
        public DbSet<T> Entities
        {
            get
            {
                return this._entities;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void RollBack()
        {
            context.Dispose();
        }

        public Repository(MyDataBaseContext context)
        {
            this.context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }
        public T GetById(int id)
        {
            return _entities.SingleOrDefault(s => s.Id == id);
        }
        public void Insert(T entity)
        {
            var errorMessage = "entity";
            if (entity == null) throw new ArgumentNullException(errorMessage);

            _entities.Add(entity);
            
        }
        public void Insert(IEnumerable<T> ts)
        {
            var errorMessage = "TEntities";
            if (ts == null) throw new ArgumentNullException(errorMessage);

            _entities.AddRange(ts);
        }
        public void Update(T entity)
        {
            var errorMessage = "entity";
            if (entity == null) throw new ArgumentNullException(errorMessage);            
        }                
        public void Update(IEnumerable<T> ts)
        {
            var errorMessage = "TEntities";
            if (ts == null) throw new ArgumentNullException(errorMessage);

            _entities.UpdateRange(ts);
        }
        public void Delete(int id)
        {
            var errorMessage = "entity";
            if (id == 0) throw new ArgumentNullException(errorMessage);

            T entity = _entities.SingleOrDefault(s => s.Id == id);
            _entities.Remove(entity);            
        }

       
    }
}
