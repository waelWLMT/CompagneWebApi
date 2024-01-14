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
    public class BillService : ServicePattern<Bill>, IBillService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IBillRepository _billRepository;

        public BillService(IBillRepository billRepo, IQuoteRepository quoteRepo) : base(billRepo)
        {
            this._quoteRepository = quoteRepo;
            this._billRepository = billRepo;
        }

        public void UpdateBillBusinessesAndProducts(ref Bill bill, Campaign campaign)
        {
            #region set bill business list
            var billBusinessList = new List<BillBusiness>();
            foreach (var item in campaign.CampaignBusinesses)
            {

                var billBusiness = new BillBusiness();

                billBusiness.BusinessName = item.Place.Name;
                billBusiness.Lat = item.Place.Lat.ToString();
                billBusiness.Lng = item.Place.Lng.ToString();

                billBusiness.TownName = campaign.CampaignTowns.Where(x => x.Id == item.BusinessTownId).FirstOrDefault().City;
                billBusiness.BusinessTypeName = campaign.CampaignBusinessTypes.Where(x => x.Id == item.BusinessTypeId).FirstOrDefault().Code;
                billBusiness.BusinessCost = this.CountBusinessCost(campaign.CampaignProducts.ToList());

                billBusinessList.Add(billBusiness);
                bill.FinalTotalCost += billBusiness.BusinessCost;


            }

            bill.NbrTowns = campaign.CampaignTowns.Count();
            bill.BillBusinesses = billBusinessList;

            #endregion

            #region set product bill list

            var productBillList = new List<BillProduct>();
            foreach (var item in campaign.CampaignProducts)
            {
                var billProduct = new BillProduct()
                {
                    ProductTypeName = item.ProductType.Name,
                    FinalUnitPrice = (float)item.FinalUnitPrice,
                    NbrProductPerBusiness = item.NbrProductPerBusiness,
                    CostPerBusiness = (float)(item.NbrProductPerBusiness * item.FinalUnitPrice),
                };

                productBillList.Add(billProduct);

            }

            bill.BillProducts = productBillList;

            #endregion

            Commit();

        }

        public float CountBusinessCost(List<Product> products)
        {
            var businessCost = (float)0;

            foreach (var product in products)
            {
                businessCost += (float)product.FinalUnitPrice * product.NbrProductPerBusiness;
            }
            return businessCost;
        }
        public Bill InitializeBill(Campaign campaign)
        {
            var bill = new Bill()
            {
                //CreatedAt = DateTime.Now,
                Customer = campaign.Customer,
                //LastModifAt = DateTime.Now,
                RegionName = campaign.Region.Name,
                CampaignId = campaign.Id,
                QuoteId = _quoteRepository.GetByCampaignId(campaign.Id).Id,
                BillBusinesses = new List<BillBusiness>()
            };

            return bill;
        }
        public int GenerateBill(Campaign campaign)
        {
            var bill = this.InitializeBill(campaign);

            this.Insert(bill);
            this.UpdateBillBusinessesAndProducts(ref bill, campaign);

            return bill.Id;
        }
        public List<Bill> GetAllFullData()
        {
            var bills = _billRepository.GetAllFullData();
            return bills;
        }
        public List<Bill> GetAllFullDataByCustomerId(int customerId)
        {
            var bills = this.GetAllFullData().Where(x => x.Customer.Id == customerId).ToList();
            return bills;
        }
        public Bill GetByCampaignId(int campaignId)
        {
            var bill = _billRepository.GetByCampaignId(campaignId);
            return bill;
        }


    }
}
