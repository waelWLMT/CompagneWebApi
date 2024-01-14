using BL.ServicePattern;
using Core.Enums;
using Core.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Services.Impl
{
    public class CampaignService : ServicePattern<Campaign>, ICampaignService
    {
        private readonly ICampaignRepository _campaignRepo;
        private readonly IRegionRepository _regionRepo;
        private readonly ITownRepository _townRepo;
        private readonly IBusninessTypeRepository _businessTypeRepo;
        private readonly IProductTypeRepository _productTypeRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IPlacesRepository _placesRepository;
        public CampaignService(
                ICampaignRepository campaignReporepo, IRegionRepository regionRepo,
                ITownRepository townRepo, IBusninessTypeRepository businessTypeRepo,
                IProductTypeRepository productTypeRepo, ICustomerRepository customerRepo,
                IPlacesRepository placesRepository
                ) : base(campaignReporepo)
        {
            _campaignRepo = campaignReporepo;
            _regionRepo = regionRepo;
            _townRepo = townRepo;
            _businessTypeRepo = businessTypeRepo;
            _productTypeRepo = productTypeRepo;
            _customerRepo = customerRepo;
            _placesRepository = placesRepository;
        }

        #region Campaign Town Management

        public Campaign AddCampaignTown(int campaignId, int townId)
        {
            var campaign = GetCampaignByIdFullData(campaignId);
            var town = _townRepo.GetById(townId);

            if (campaign != null && town != null && !campaign.CampaignTowns.Contains(town))
            {
                campaign.CampaignTowns.Add(town);
                var townBusinesses = GetTownBusinesses(campaign, town, campaign.CampaignBusinessTypes.ToList());

                foreach (var business in townBusinesses)
                    campaign.CampaignBusinesses.Add(business);

                campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                Commit();

            }

            return campaign;
        }

        public Campaign DeleteCampaignTown(int campaignId, int townId)
        {
            var campaign = GetCampaignByIdFullData(campaignId);

            if (campaign != null)
            {
                var town = campaign.CampaignTowns.Where(x => x.Id == townId).FirstOrDefault();
                if (town != null)
                {
                    // remove town
                    campaign.CampaignTowns.Remove(town);

                    // get valid businesses
                    var campaignBusinesses = campaign.CampaignBusinesses.Where(b => b.BusinessTownId != townId).ToList();

                    // set campaign Businesses
                    campaign.CampaignBusinesses = campaignBusinesses;

                    // recount campaign totalCost
                    campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                    Commit();
                }
            }

            return campaign;
        }

        // Get list of detailed campaign towns
        public List<DetailsCampaignTown> GetListDetailsCampaignTowns(int campaignId)
        {
            var campaign = GetCampaignByIdFullData(campaignId);

            var list = new List<DetailsCampaignTown>();

            if (campaign != null && campaign.CampaignTowns != null)
            {
                var towns = campaign.CampaignTowns;
                foreach (var town in towns)
                {
                    var businesses = campaign.CampaignBusinesses.Where(x => x.BusinessTownId == town.Id).ToList();
                    var nbrBusinesses = businesses.Count();
                    var townCost = CountBusinessTypeCost(campaign.CampaignProducts) * nbrBusinesses;
                    townCost = townCost * campaign.PenetraionRate / 100;

                    var townDetails = new DetailsCampaignTown()
                    {
                        Town = town,
                        NbrBusinesses = nbrBusinesses,
                        TownCost = townCost,
                        TownBusinesses = businesses,
                        PenetrationRate = campaign.PenetraionRate
                    };

                    list.Add(townDetails);
                }
            }

            return list;
        }

        #endregion

        #region Campaign Global Params Management
        public Campaign UpdateCampaignGlobalParameters(int campaignId, Campaign campaignModif)
        {
            var campaign = GetCampaignByIdFullData(campaignId);

            if (campaign != null)
            {
                // set new data
                campaign.Title = campaignModif.Title;
                campaign.Goal = campaignModif.Goal;
                campaign.ExecutionDate = campaignModif.ExecutionDate;
                campaign.PenetraionRate = campaignModif.PenetraionRate;
                campaign.ForecastBudget = campaignModif.ForecastBudget;
                campaign.Description = campaignModif.Description;

                // count the new value of campaign totalCost
                campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                Commit();
            }

            return campaign;
        }

        // Count businessType Total Cost 
        public double CountBusinessTypeCost(ICollection<Product> products)
        {
            var businessTypeCost = 0.0;

            foreach (var product in products)
            {
                var productCostPerBusiness = product.NbrProductPerBusiness * product.FinalUnitPrice;
                businessTypeCost += productCostPerBusiness;
            }

            return businessTypeCost;

        }

        // Count Campaign TotalCost
        public double CountCampaignTotalCost(Campaign campaign)
        {

            var businessTypeCost = CountBusinessTypeCost(campaign.CampaignProducts);
            var nbrCampaignBusiness = campaign.CampaignBusinesses.Count() * campaign.PenetraionRate / 100;

            return nbrCampaignBusiness * businessTypeCost;

        }

        #endregion

        #region Campaign Businesses and BusinessTypes  Management

        public Campaign AddCampaignBusinessType(int campaignId, string businessTypeMapCode)
        {
            var campaign = GetCampaignByIdFullData(campaignId);

            if (campaign != null)
            {
                var listMapCodes = new List<string>() { businessTypeMapCode };

                var businessType = _businessTypeRepo.GetBusinessInListMapCodes(listMapCodes).FirstOrDefault();

                if (businessType != null && !campaign.CampaignBusinessTypes.Contains(businessType))
                {
                    campaign.CampaignBusinessTypes.Add(businessType);

                    // update campaignBusinesses
                    foreach (var town in campaign.CampaignTowns)
                    {
                        var places = _placesRepository.GetPlacesList(town, businessType).ToList<Place>();

                        foreach (var place in places)
                        {
                            var campaignBusiness = new CampaignBusiness()
                            {
                                BusinessType = businessType,
                                Campaign = campaign,
                                BusinessTypeId = businessType.Id,
                                CompagnId = campaign.Id,
                                Place = place,
                                Photos = new HashSet<Photo>(),
                                State = BusinessState.A_Faire,
                                BusinessTownId = town.Id
                            };

                            campaign.CampaignBusinesses.Add(campaignBusiness);
                        }
                    }

                    campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                    Commit();
                }


            }

            return campaign;
        }
        public Campaign DeleteCampaignBusinessType(int campaignId, string BusinessTypeMapCode)
        {
            var campaign = this.GetCampaignByIdFullData(campaignId);
            var businessType = campaign.CampaignBusinessTypes.FirstOrDefault(x => x.MapCode == BusinessTypeMapCode);

            if (businessType != null)
            {
                // remove campaigne Business
                campaign.CampaignBusinessTypes.Remove(businessType);

                // update campaignBusinesses
                var validBusinesses = campaign.CampaignBusinesses.Where(x => x.BusinessTypeId != businessType.Id).ToList();
                campaign.CampaignBusinesses = validBusinesses;

                campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                Commit();
            }

            return campaign;
        }
        public void InitCampaignBusinesses(ref Campaign campaign)
        {
            var errorMsg = "";

            var businessTypes = campaign.CampaignBusinessTypes;
            var towns = campaign.CampaignTowns;

            if (businessTypes == null || businessTypes.Count() <= 0)
            {
                errorMsg = "le compagne ne contient pas des type de business!!";
                throw new Exception(errorMsg);
            }

            if (towns == null || towns.Count() <= 0)
            {
                errorMsg = "le compagne ne contient pas des villes!!";
                throw new Exception(errorMsg);
            }

            campaign.CampaignBusinesses = new HashSet<CampaignBusiness>();

            foreach (var town in towns)
            {
                var businesses = GetTownBusinesses(campaign, town, campaign.CampaignBusinessTypes.ToList());

                foreach (var item in businesses)
                    campaign.CampaignBusinesses.Add(item);


                //campaign.CampaignBusinesses.ToList().AddRange(businesses);               
            }

        }
        public List<CampaignBusiness> GetTownBusinesses(Campaign campaign, Town town, List<BusinessType> businessTypes)
        {
            var TownBusinesses = new HashSet<CampaignBusiness>();

            foreach (var businessType in businessTypes)
            {
                var places = _placesRepository.GetPlacesList(town, businessType).ToList<Place>();

                foreach (var place in places)
                {
                    var campaignBusiness = new CampaignBusiness()
                    {
                        BusinessType = businessType,
                        Campaign = campaign,
                        BusinessTypeId = businessType.Id,
                        CompagnId = campaign.Id,
                        Place = place,
                        Photos = new HashSet<Photo>(),
                        State = BusinessState.A_Faire,
                        BusinessTownId = town.Id
                    };

                    TownBusinesses.Add(campaignBusiness);
                }
            }

            return TownBusinesses.ToList();
        }

        #endregion

        #region Campaign Products Management

        public void InitCampaignProducts(ref Campaign campaign, List<int> productTypesIds)
        {
            var products = new HashSet<Product>();
            var productTypes = _productTypeRepo.GetproductTypeInListIds(productTypesIds);

            foreach (var item in productTypes)
            {
                var product = new Product()
                {
                    ProductType = item,
                    NbrProductPerBusiness = item.DefaultNbrProductPerBusiness,
                    FinalUnitPrice = item.Price,
                    CampaignId = campaign.Id,
                    ProductTypeId = item.Id,
                    Campaign = campaign
                };

                products.Add(product);
            }

            campaign.CampaignProducts = products;
        }
        public Campaign AddCampaignProduct(int campaignId, int productTypeId)
        {
            var campaign = GetCampaignByIdFullData(campaignId);

            if (campaign != null)
            {
                var productType = _productTypeRepo.GetById(productTypeId);

                var campaignProduct = new Product()
                {
                    ProductTypeId = productType.Id,
                    CampaignId = campaign.Id,
                    ProductType = productType,
                    FinalUnitPrice = productType.Price,
                    NbrProductPerBusiness = productType.DefaultNbrProductPerBusiness
                };

                if (!campaign.CampaignProducts.Contains(campaignProduct))
                {
                    campaign.CampaignProducts.Add(campaignProduct);
                    campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                    Commit();
                }

            }

            return campaign;
        }
        public Campaign UpdateCampaignProduct(int campaignId, int productTypeId, int finalNbrProductPerBusiness, float finalPrice)
        {
            var campaign = _campaignRepo.GetCampaignFullData(campaignId);

            var product = campaign.CampaignProducts.FirstOrDefault(x => x.ProductTypeId == productTypeId);

            if (product != null)
            {
                product.NbrProductPerBusiness = finalNbrProductPerBusiness;
                product.FinalUnitPrice = Math.Round(finalPrice, 2);

                campaign.CampaignProducts.Remove(product);
                campaign.CampaignProducts.Add(product);
                campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                Commit();
            }

            return campaign;
        }

        public Campaign DeleteCampaignProduct(int campaignId, int productTypeId)
        {

            var campaign = this.GetCampaignByIdFullData(campaignId);
            var product = campaign.CampaignProducts.FirstOrDefault(x => x.ProductTypeId == productTypeId);

            if (campaign != null && product != null)
            {
                campaign.CampaignProducts.Remove(product);
                campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                Commit();
            }




            return campaign;

        }

        #endregion

        #region Campaign Management
        public int CreateCampaign(Campaign campaign, int regionId, List<int> townsIds, List<string> businessTypesIds, List<int> productTypeIds, int customerId)
        {
            try
            {
                // Initial state
                campaign.CampaignState = CampaignState.Brouillon;

                // Set campaign params
                campaign.Customer = _customerRepo.GetById(customerId);
                campaign.Region = _regionRepo.GetById(regionId);
                campaign.CampaignTowns = _townRepo.GetTownsInListIds(townsIds);
                campaign.CampaignBusinessTypes = _businessTypeRepo.GetBusinessInListMapCodes(businessTypesIds);

                campaign.PenetraionRate = 100;

                Insert(campaign);
                Commit();

                // Set campaign products
                InitCampaignProducts(ref campaign, productTypeIds);

                // Set campaign Businesses
                InitCampaignBusinesses(ref campaign);

                campaign.TotalCost = (float)CountCampaignTotalCost(campaign);

                Commit();

                return campaign.Id;
            }
            catch (Exception ex)
            {
                RollBack();
                var msg = ex.Message;
                return -1;
            }

        }
        public Campaign GetCampaignByIdFullData(int idCampaign)
        {
            var campaign = _campaignRepo.GetCampaignFullData(idCampaign);
            return campaign;
        }
        public List<Campaign> GetAllCampaigns()
        {
            var campaigns = _campaignRepo.GetAllCampaigns();

            return campaigns;
        }

        public Campaign initDuplicatedCampaign(Campaign oldCampaign, int userId)
        {
            var campaign = new Campaign();

            campaign.Title = oldCampaign.Title;
            campaign.Goal = oldCampaign.Goal;
            campaign.ForecastBudget = oldCampaign.ForecastBudget;
            campaign.PenetraionRate = oldCampaign.PenetraionRate;

            campaign.Description = oldCampaign.Description;
            campaign.TotalCost = 0;
            campaign.ExecutionDate = DateTime.Today.AddDays(30);
            campaign.UserId = userId;

            return campaign;
        }

        public int DuplicateCampaign(int campaignId, int userId)
        {
            var oldCampaign = this.GetCampaignByIdFullData(campaignId);

            var townsList = oldCampaign.CampaignTowns.Select(x => x.Id).ToList();
            var businessTypesListCodes = oldCampaign.CampaignBusinessTypes.Select(x => x.MapCode).ToList();
            var productTypeListIds = oldCampaign.CampaignProducts.Select(x => x.ProductTypeId).ToList();
            var regionId = oldCampaign.RegionId;
            var customerId = oldCampaign.CustomerId;

            var newCampaign = this.initDuplicatedCampaign(oldCampaign, userId);


            var newCampagnId = this.CreateCampaign(newCampaign, regionId, townsList, businessTypesListCodes, productTypeListIds, customerId);

            return newCampagnId;
        }

        public List<Campaign> SearchCampaignByCreteria(DateTime? startDate, DateTime? endDate, int? clientId, int? regionId, List<int> towns, List<int> businessTypes)
        {
            var campaigns = _campaignRepo.SearchByCreteria(startDate, endDate, clientId, regionId, towns, businessTypes);
            return campaigns;
        }

        public Campaign LaunchCampaignRealization(int campaignId, int userId)
        {
            var campaign = this.GetCampaignByIdFullData(campaignId);

            if (campaign.CampaignState == CampaignState.ValidéeParClient)
            {
                campaign.CampaignState = CampaignState.EnCours;
                campaign.ExecutionDate = DateTime.Today;
                campaign.LastModifAt = DateTime.Now;
                campaign.LastUserModifId = userId;

                Commit();
            }

            return campaign;
        }


        // this Method updates campaign state to Cloturée
        public Campaign CloseCampaign(int campaignId, int userId)
        {
            var campaign = this.GetCampaignByIdFullData(campaignId);

            // update campaign State
            if (campaign.CampaignState == CampaignState.EnCours)
            {
                campaign.CampaignState = CampaignState.Clôturée;
                campaign.ExecutionDate = DateTime.Today;
                campaign.LastModifAt = DateTime.Now;
                campaign.LastUserModifId = userId;

                Commit();
            }


            return campaign;
        }

        public CampaignBusiness UpdateCampaignBusinessState(int campaignId, int newStateId, int userModifId, int campaignBusinessId)
        {

            var campaign = this.GetCampaignByIdFullData(campaignId);

            if (campaign != null)
            {
                var business = campaign.CampaignBusinesses.Where(x => x.CampaignBusinessId == campaignBusinessId).FirstOrDefault();
                business.State = (BusinessState)newStateId;
                business.UserModifId = userModifId;
                business.LastDateModif = DateTime.Now;

                Commit();

                return business;
            }

            return null;
            
        }

        #endregion

    }
}
