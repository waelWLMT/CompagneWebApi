using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDataBaseContext _databaseContext;      

        public UnitOfWork(MyDataBaseContext databaseContext)
        { _databaseContext = databaseContext; }       

        public void Commit() { 
            _databaseContext.SaveChanges(); 
        }

        public void Rollback() {
            _databaseContext.Dispose();
        }
    }
}
