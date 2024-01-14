using Core.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ServicePattern
{
    public interface IServicePattern <T> where T: BaseEntity {

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
