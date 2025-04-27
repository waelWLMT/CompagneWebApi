using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Core.Models;
using Data.Repositories;
using Data.Repositories.Impl;
using Reporting.RptDataSets;

namespace BL.Services.Impl
{
    public class BillingReportService : IBillingReportService
    {

        private readonly IQuoteRepository _quoteRepository;
        private readonly ICampaignRepository _campagneRepository;
        private readonly IBillRepository _billRepository;
        private string mimtype = "";
        private int extension = 1;

        public BillingReportService(IBillRepository billRepository, IQuoteRepository quoteRepository, ICampaignRepository campaignRepository)
        {
            _quoteRepository = quoteRepository;
            _campagneRepository = campaignRepository;
            _billRepository = billRepository;
        }

        #region Devis reporting
        private Dictionary<string, string> getReportParameters(Campaign campaign, Quote quote, bool isBill)
        {
            var reportParams = new Dictionary<string, string>();

            if (isBill)
            {
                reportParams.Add("RptNumeroTxt", "facture");
                reportParams.Add("RptTitle", "Facture");
                reportParams.Add("RptDateTxt", "facture");
            }
            else
            {
                reportParams.Add("RptNumeroTxt", "devis");
                reportParams.Add("RptTitle", "Devis");
                reportParams.Add("RptDateTxt", "devis");
            }              

            reportParams.Add("RptNumero", quote.Id.ToString());
            reportParams.Add("RptNumeroClient", campaign.Customer.Id.ToString());

            reportParams.Add("RptRegion", quote.RegionName);
            reportParams.Add("RptDate", quote.CreatedAt.ToString("dd-MM-yyyy"));
            reportParams.Add("RptClientName", quote.CustomerName);
            reportParams.Add("RptClientContact", campaign.Customer.Mail);

            StringBuilder sbAddresse = new StringBuilder(campaign.Customer.Address.HouseNumber + " ");
            sbAddresse.Append(campaign.Customer.Address.Street + " ");
            sbAddresse.Append(campaign.Customer.Address.TownName + " ");
            sbAddresse.Append(campaign.Customer.Address.PostalCode + " ");
            sbAddresse.Append(campaign.Customer.Address.CountryName);

            reportParams.Add("RptClientAdresse", sbAddresse.ToString());
            reportParams.Add("RptTotalCost", quote.TotalCost.ToString());

            return reportParams;
        }

        private DevisDataSet getDevisDataSet(Campaign campaign, Quote quote)
        {
            var ds = new DevisDataSet();
            ds.DevisTowns.Clear();
            ds.DevisProduct.Clear();
            ds.DevisBusinessType.Clear();
            ds.Clear();

            ds.DataSetName = "DevisDataSet";

            // products
            foreach (var item in quote.ProductQuoteLines)
            {
                ds.DevisProduct.AddDevisProductRow(item.Id, item.ProductTypeName, item.NbrProductPerBusiness, item.CostPerBusiness);
            }

            // business types
            foreach (var item in quote.BusinessTypeQuoteLines)
            {
                ds.DevisBusinessType.AddDevisBusinessTypeRow(item.Id, item.BusinessTypeDesignation, item.BusinessCost, item.NbrBusinessTypePerCampagne);
            }

            // towns
            foreach (var item in campaign.CampaignTowns)
            {
                var townCost = 10.2; // à fixer et trouver une solution pour le calculer
                ds.DevisTowns.AddDevisTownsRow(item.Id, item.City, townCost, 100);
            }

            return ds;

        }

        public byte[] GenerateCampaignDevisReport(int devisId)
        {            
            
            var quote = _quoteRepository.GetQuoteFullDataById(devisId);
            var campaign = _campagneRepository.GetCampaignFullData(quote.CampaignId);

            var ds = getDevisDataSet(campaign, quote);

            var reportParams = getReportParameters(campaign, quote, false);

            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "DevisReport.rdlc");

            byte[] file = null;

            var localReport = new LocalReport(reportPath);

            // set report dataset
            localReport.AddDataSource("DevisBusinessTypes", ds.DevisBusinessType);
            localReport.AddDataSource("DevisProducts", ds.DevisProduct);
            localReport.AddDataSource("DevisTowns", ds.DevisTowns);

            // execute report
            var result = localReport.Execute(RenderType.Pdf, extension, reportParams, mimtype);

            // create file as byte [] 
            file = result.MainStream;

            // return byte[]
            return file;

        }

        public byte[] GenerateBillingReport(int id, bool isBill)
        {
            var quote = _quoteRepository.GetQuoteFullDataById(id);
            
            if (isBill)
            {
                var bill = _billRepository.GetById(id);
                quote = _quoteRepository.GetQuoteFullDataById(bill.QuoteId);
            }            
            
            var campaign = _campagneRepository.GetCampaignFullData(quote.CampaignId);

            var ds = getDevisDataSet(campaign, quote);
            var reportParams = getReportParameters(campaign, quote, isBill);

            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "BillingReport.rdlc");

            byte[] file = null;

            var localReport = new LocalReport(reportPath);

            // set report dataset
            localReport.AddDataSource("BusinessTypesDs", ds.DevisBusinessType);
            localReport.AddDataSource("ProductsDs", ds.DevisProduct);
            localReport.AddDataSource("TownsDs", ds.DevisTowns);

            // execute report
            var result = localReport.Execute(RenderType.Pdf, extension, reportParams, mimtype);

            // create file as byte [] 
            file = result.MainStream;

            // return byte[]
            return file;
        }

        public byte[] GenerateBillingReportByCampagnId(int campaignId, bool isBill)
        {
            if (isBill)
            {
                var factureId = _billRepository.GetByCampaignId(campaignId).Id;
                return GenerateBillingReport(factureId, true);
            }

            var devisId = _quoteRepository.GetByCampaignId(campaignId).Id;
            return GenerateBillingReport(devisId, false);
        }


        #endregion

    }
}
