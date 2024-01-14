using BL.ServicePattern;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public interface IBillService : IServicePattern<Bill>
    {
        void UpdateBillBusinessesAndProducts(ref Bill bill, Campaign campaign);
        float CountBusinessCost(List<Product> products);
        Bill InitializeBill(Campaign campaign);
        int GenerateBill(Campaign campaign);
        List<Bill> GetAllFullData();
        List<Bill> GetAllFullDataByCustomerId(int customerId);
        Bill GetByCampaignId(int campaignId);


    }
}
