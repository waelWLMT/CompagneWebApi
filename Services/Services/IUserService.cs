using BL.ServicePattern;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public interface IUserService : IServicePattern<User>
    {
        void DesactivateUser(int userId, bool activate);

        User GetUserByUserName(string login);
    }
}
