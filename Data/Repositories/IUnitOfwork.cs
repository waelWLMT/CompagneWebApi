using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IUnitOfWork
    {       
        void Commit();
        void Rollback();
    }
}
