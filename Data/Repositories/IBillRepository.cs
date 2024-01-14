using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IBillRepository : IRepository<Bill>
    {
        List<Bill> GetAllFullData();
        Bill GetByCampaignId(int campaignId);
    }
}
