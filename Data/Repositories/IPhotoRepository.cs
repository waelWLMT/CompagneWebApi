using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        public List<Photo> GetPhotoByBusiness(int businessId);
    }
}
