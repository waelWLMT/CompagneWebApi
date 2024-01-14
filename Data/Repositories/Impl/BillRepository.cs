using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository(MyDataBaseContext context) : base(context) { }

        public List<Bill> GetAllFullData()
        {
            var bills = this.Entities
                .Include(x => x.Customer)
                .Include(x=> x.BillBusinesses)                
                .ToList();

            return bills;
        }

        public Bill GetByCampaignId(int campaignId)
        {
            var bill = this.Entities
                .Where(x => x.CampaignId == campaignId)
                .Include(x => x.Customer)
                .Include(x => x.BillBusinesses)
                .Include(x=> x.BillProducts)
                .FirstOrDefault();

            return bill;
        }
    }
}
