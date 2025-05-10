using BL.ServicePattern;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ICampaignService : IServicePattern<Campaign>
    {

        DetailsCampaignTown GetListDetailedCampaignTown(int campaignId, int townId);
        Task<List<CampaignBusiness>> GetTownBusinesses(Campaign campaign, List<Town> towns, List<BusinessType> businessTypes);
        Task<Campaign> AddCampaignTown(int campaignId, int townId);
        Campaign DeleteCampaignTown(int campaignId, int townId);
        List<Campaign> GetAllCampaigns();
        Campaign UpdateCampaignGlobalParameters(int campaignId, Campaign campaignModif);
        double CountBusinessTypeCost(ICollection<Product> products);
        double CountCampaignTotalCost(Campaign campaign);
        Campaign UpdateCampaignProduct(int campaignId, int productTypeId, int finalNbrProductPerBusiness, float finalPrice);
        Campaign DeleteCampaignBusinessType(int campaignId, int BusinessTypeId);
        Campaign DeleteCampaignProduct(int campaignId, int productTypeId);
        Campaign GetCampaignByIdFullData(int idCampaign);
        Campaign CloseCampaign(int campaignId, int userId);
        List<Campaign> SearchCampaignByCreteria(DateTime? startDate, DateTime? endDate, int? clientId, int? regionId, List<int> towns, List<int> businessTypes);
        Campaign LaunchCampaignRealization(int campaignId, int userId);
        Campaign initDuplicatedCampaign(Campaign oldCampaign, int userId);
        Task<int> DuplicateCampaign(int campaignId, int userId);
        Task InitCampaignBusinesses(Campaign campaign);
        void InitCampaignProducts(ref Campaign campaign, List<int> productTypesIds);
        Task<int> CreateCampaign(Campaign campaign, int regionId, List<int> townsIds, List<string> businessTypesIds, List<int> productTypeIds, int customerId);
        Campaign AddCampaignProduct(int campaignId, int productTypeId);
        Task<Campaign> AddCampaignBusinessType(int campaignId, int businessTypeId);
        List<DetailsCampaignTown> GetListDetailsCampaignTowns(int campaignId);
        CampaignBusiness UpdateCampaignBusinessState(int campaignId, int newStateId, int userModifId, int campaignBusinessId);
    }
}
