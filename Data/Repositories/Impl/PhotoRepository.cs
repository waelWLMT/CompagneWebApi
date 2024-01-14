using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class PhotoRepository: Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(MyDataBaseContext ctx): base(ctx) { }

        public List<Photo> GetPhotoByBusiness(int businessId)
        {
            var list = base.Entities
                .Include(x=> x.CampaignBusiness)
                .Where(x => x.CampaignBusiness.CampaignBusinessId == businessId)
                .ToList();

            return list;
        }
    }
}
