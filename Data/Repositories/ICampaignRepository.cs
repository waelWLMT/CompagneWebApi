using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        List<Campaign> GetAllCampaigns();
        Campaign GetCampaignFullData(int idCampaign);
        List<Campaign> SearchByCreteria(DateTime? startDate, DateTime? endDate, int? clientId, int? regionId, List<int> towns, List<int> businessTypes);
        
    }
}
