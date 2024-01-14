using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class QuoteRepository: Repository<Quote>, IQuoteRepository
    {
        public QuoteRepository(MyDataBaseContext ctx): base(ctx) {  }

        public Quote GetByCampaignId(int campaignId)
        {
            var quote = this.Entities.Where(x => x.CampaignId == campaignId).FirstOrDefault();
            return quote;
        }

        public Quote GetQuoteFullDataById(int devisId)
        {
            var quote = base.Entities.Where(x => x.Id == devisId)
                .Include(x => x.BusinessTypeQuoteLines)
                .Include(x => x.ProductQuoteLines)
                .FirstOrDefault();

            return quote;         

        }
    }
}
