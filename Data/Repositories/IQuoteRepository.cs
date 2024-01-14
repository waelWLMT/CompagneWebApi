using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IQuoteRepository : IRepository<Quote>
    {
        Quote GetQuoteFullDataById(int devisId);
        Quote GetByCampaignId(int campaignId);
        
    }
}
