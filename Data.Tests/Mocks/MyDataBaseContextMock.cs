using Core.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tests.Mocks
{
    public class MyDataBaseContextMock<T>
    {
        private MyDataBaseContextMock<MyDataBaseContext> getDbContext(Campaign[] initialEntities)
        {
            var dbContextMock = new MyDataBaseContextMock<MyDataBaseContext>();

            dbContextMock.
            return dbContextMock;
        }
    }
}
