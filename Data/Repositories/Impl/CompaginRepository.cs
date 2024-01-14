using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Impl
{
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(MyDataBaseContext ctx) : base(ctx)
        { }

        public List<Campaign> SearchByCreteria(DateTime? startDate, DateTime? endDate, int? clientId, int? regionId, List<int> towns, List<int> businessTypes)
        {           

            // Native query
            var query = "SELECT * FROM Campaigns ";           
           
            query += " WHERE (CreatedAt >= CONVERT(datetime, '" + startDate.Value.ToString("yyyy-MM-dd") + " '))";
            query += " AND (CreatedAt <= CONVERT(datetime, '"+endDate.Value.ToString("yyyy-MM-dd")+ "')) ";
                        
            if (clientId != null)
                query += " AND (CustomerId = " + clientId.Value + ")";

            if (regionId != null)
                query += " AND (RegionId = "+ regionId.Value+ ") ";           

            var initialResult = this.Entities.FromSqlRaw(query)
                .Include(x => x.CampaignTowns)
                .Include(x => x.CampaignBusinessTypes)
                .Include(x => x.Customer)
                .Include(x => x.Region);

            var result = initialResult.OrderByDescending(x=> x.LastModifAt).ToList();
            
            // campaigns who have any town of towns parameters
            if (towns != null && towns.Count > 0)
            {                
                var filtredResult = new List<Campaign>();
                
                foreach(var campaign in result)
                {
                    var campaignTownIds = campaign.CampaignTowns.Select(x => x.Id).ToList();
                    
                    if (campaignTownIds.Intersect(towns).Any())
                        filtredResult.Add(campaign);
                }

                result = filtredResult;
            }

            // campaigns who have any business type of business types parameters
            if (businessTypes != null && businessTypes.Count() > 0)
            {
                var filtredResult = new List<Campaign>();

                foreach (var campaign in result)
                {
                    var campaignBusinessTypesIds = campaign.CampaignBusinessTypes.Select(x => x.Id).ToList();

                    if (campaignBusinessTypesIds.Intersect(businessTypes).Any())
                        filtredResult.Add(campaign);
                }

                result = filtredResult;
            }
           
            return result;

        }

        public List<Campaign> GetAllCampaigns()
        {
            var campaigns = Entities
                .Include(x => x.Customer)
                .Include(x => x.Region)
                .Include(x=> x.CampaignTowns)
                .OrderByDescending(x=> x.LastModifAt)
                .ToList();

            return campaigns;
        }

        public Campaign GetCampaignFullData(int idCampaign)
        {
            var campaign = base.Entities
                .Where(x => x.Id == idCampaign)
                .Include(x => x.Customer)
                .Include(x => x.Region)
                .Include(x => x.CampaignBusinessTypes)
                .Include(x => x.CampaignBusinesses.OrderByDescending(x=> x.LastDateModif)).ThenInclude(x=> x.Place)
                .Include(x => x.CampaignTowns)
                .Include(x => x.CampaignProducts)
                .Include(x => x.CampaignProducts).ThenInclude(pt => pt.ProductType);

            return campaign.FirstOrDefault();

        }

    }
}
