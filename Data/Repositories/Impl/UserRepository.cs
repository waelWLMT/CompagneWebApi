using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyDataBaseContext context) : base(context) { }

        public User GetUserByUserName(string login)
        {
            var user = Entities.Where(x => x.Login == login).SingleOrDefault();

            return user;
        }
    }
}
