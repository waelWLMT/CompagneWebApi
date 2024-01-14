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
    public class QuoteService : ServicePattern<Quote>, IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly ICampaignService _campaignService;

        public QuoteService(IQuoteRepository quoteRepository, ICampaignService campaignService) : base(quoteRepository)
        {
            this._quoteRepository = quoteRepository;
            this._campaignService = campaignService;
        }

        public void CreateDevis(int campaignId)
        {
            var campaign = this._campaignService.GetCampaignByIdFullData(campaignId);

            if (campaign != null)
            {
                var devis = this.InitialiseQuote(campaign);

                base.Insert(devis);
                base.Commit();

                this.SetProductLines(ref devis, campaign);
                this.SetBusinessTypesLines(ref devis, campaign);

                campaign.CampaignState = CampaignState.ValidéeParClient;

                base.Commit();

                // Send Notif Mail To Responsable Afin de lancer la Réalisation de la compagne
                // à complété
            }

        }

        public Quote InitialiseQuote(Campaign campaign)
        {
            var devis = new Quote();

            devis.CreatedAt = DateTime.Now;
            devis.LastModifAt = DateTime.Now;

            devis.CustomerId = campaign.CustomerId;
            devis.CustomerName = campaign.Customer.Name;
            devis.RegionName = campaign.Region.Name;
            devis.NbrBusinesses = campaign.CampaignBusinesses.Count();
            devis.PenetrationRate = campaign.PenetraionRate;

            devis.State = QuoteState.Validé;
            devis.TotalCost = campaign.TotalCost;
            devis.TownsNumber = campaign.CampaignTowns.Count();
            devis.CampaignId = campaign.Id;

            return devis;
        }

        public void SetProductLines(ref Quote devis, Campaign campaign)
        {
            // Product Lines
            devis.ProductQuoteLines = new List<ProductQuoteLine>();

            var businessCost = 0.0;

            foreach (var product in campaign.CampaignProducts)
            {
                var ProductLine = new ProductQuoteLine()
                {
                    CreatedAt = DateTime.Now,
                    LastModifAt = DateTime.Now,
                    QuoteId = devis.Id,
                    ProductTypeId = product.ProductTypeId,
                    ProductTypeName = product.ProductType.Name,
                    NbrProductPerBusiness = product.NbrProductPerBusiness,
                    UnitPrice = (float)product.FinalUnitPrice,
                    CostPerBusiness = (float)product.FinalUnitPrice * product.NbrProductPerBusiness
                };
                businessCost += ProductLine.CostPerBusiness;
                devis.ProductQuoteLines.Add(ProductLine);
            }

            devis.BusinessCost = (float)businessCost;

        }

        public void SetBusinessTypesLines(ref Quote devis, Campaign campaign)
        {
            //Business Type Lines
            devis.BusinessTypeQuoteLines = new List<BusinessTypeQuoteLine>();

            foreach (var businessType in campaign.CampaignBusinessTypes)
            {
                var businessTypeLine = new BusinessTypeQuoteLine()
                {
                    CreatedAt = DateTime.Now,
                    LastModifAt = DateTime.Now,

                    QuoteId = devis.Id,
                    BusinessTypeId = businessType.Id,
                    BusinessTypeCode = businessType.Code,
                    BusinessCost = devis.BusinessCost
                };

                var nbrBussinessTypePerCampagne = campaign.CampaignBusinesses.Where(x => x.BusinessTypeId == businessType.Id).Count();
                businessTypeLine.NbrBusinessTypePerCampagne = nbrBussinessTypePerCampagne;
                devis.BusinessTypeQuoteLines.Add(businessTypeLine);
            }

        }

        public List<Quote> GetQuotesByRoleUser(int userRoleId, int customerId)
        {
            var list = new List<Quote>();

            if (userRoleId == 1)
                list = this.GetAll().ToList();
            else            
                list = _quoteRepository.GetAll().Where(x => x.CustomerId == customerId).ToList();

            return list;
        }

        public Quote GetQuoteFullDataById(int devisId)
        {
            var quote = this._quoteRepository.GetQuoteFullDataById(devisId);

            return quote;
        }

        public Quote GetQuoteByCamapignId(int campaignId)
        {
            var quote = _quoteRepository.GetByCampaignId(campaignId);
            return quote;
        }
    }
}
