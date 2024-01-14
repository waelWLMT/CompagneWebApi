using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUserName(string login);
    }
}
