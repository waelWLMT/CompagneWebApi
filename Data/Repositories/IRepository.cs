using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Insert(IEnumerable<T> ts);
        void Update(T entity);
        void Update(IEnumerable<T> ts);
        void Delete(int id); 
        void Commit();
        void RollBack();       
    }
}
