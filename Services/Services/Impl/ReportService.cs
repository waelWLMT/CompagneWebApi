
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AspNetCore.Reporting;
using AspNetCore.Reporting.ReportExecutionService;
using Core.Models;
using Data.Repositories;
using Data.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using Reporting;
using Reporting.RptDataSets.Devis;
using Reporting.RptDataSets.Facture;
using static Reporting.RptDataSets.Devis.DevisDataSet;

namespace BL.Services.Impl
{
    public class ReportService : IReportService
    {
        private readonly IQuoteRepository _quoteReposotry;
        private readonly ICampaignRepository _campaignReposotry;
        private readonly IBillRepository _billRepository;

        private string mimtype = "";
        private int extension = 1;

        public ReportService(IBillRepository billRepository, IQuoteRepository quoteReposotry, ICampaignRepository campaignReposotry)
        {
            _quoteReposotry = quoteReposotry;
            _campaignReposotry = campaignReposotry;
            _billRepository = billRepository;
        }

        #region Devis reporting
        private Dictionary<string, string> getDevisReportDictionnaryParameters(Campaign campaign, Quote quote)
        {
            var reportParams = new Dictionary<string, string>();

            reportParams.Add("RptTitle", "Devis");
            reportParams.Add("RptNumeroDevis", quote.Id.ToString());
            reportParams.Add("RptNumeroClient", campaign.Customer.Id.ToString());

            reportParams.Add("RptRegion", quote.RegionName);
            reportParams.Add("RptDateDevis", quote.CreatedAt.ToString("dd-MM-yyyy"));
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
            var quote = _quoteReposotry.GetQuoteFullDataById(devisId);
            var campaign = _campaignReposotry.GetCampaignFullData(quote.CampaignId);

            if (quote == null || campaign == null)
                return null;

            var ds = getDevisDataSet(campaign, quote);
            var reportParams = getDevisReportDictionnaryParameters(campaign,quote);       
                
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "DevisReport.rdlc");
            var localReport = new LocalReport(reportPath);
            
            var parameters = new List<ReportParameter>();

            //// Set report parameters
            foreach (var item in reportParams)
                parameters.Add(new ReportParameter());

            // set report dataset
            localReport.AddDataSource("BusinessTypes", ds.DevisBusinessType);
            localReport.AddDataSource("Products", ds.DevisProduct);
            localReport.AddDataSource("Towns", ds.DevisTowns);

            // execute report
            var result = localReport.Execute(RenderType.Pdf, extension, reportParams, mimtype);

            // create file as byte [] 
            byte[] file = result.MainStream;

            // return byte[]
            return file;

        }
        #endregion

        #region Facture reporting

        private Dictionary<string, string> getFactureReportDictionnaryParameters(Campaign campaign, Bill bill)
        {
            var reportParams = new Dictionary<string, string>();

            reportParams.Add("RptTitle", "Facture");
            reportParams.Add("RptNumeroFacture", bill.Id.ToString());
            reportParams.Add("RptNumeroClient", campaign.Customer.Id.ToString());

            reportParams.Add("RptRegion", bill.RegionName);
            reportParams.Add("RptDateFacture", bill.CreatedAt.ToString("dd-MM-yyyy"));
            reportParams.Add("RptClientName", campaign.Customer.Name);
            reportParams.Add("RptClientContact", campaign.Customer.Mail);

            StringBuilder sbAddresse = new StringBuilder(campaign.Customer.Address.HouseNumber + " ");
            sbAddresse.Append(campaign.Customer.Address.Street + " ");
            sbAddresse.Append(campaign.Customer.Address.TownName + " ");
            sbAddresse.Append(campaign.Customer.Address.PostalCode + " ");
            sbAddresse.Append(campaign.Customer.Address.CountryName);

            reportParams.Add("RptClientAdresse", sbAddresse.ToString());
            reportParams.Add("RptTotalCost", bill.FinalTotalCost.ToString());

            return reportParams;
        }

        private FactureDataSet getFactureDataSet(Campaign campaign, Bill bill)
        {
            var ds = new FactureDataSet();
            ds.DataSetName = "FactureDataSet";            

            // products
            foreach (var item in bill.BillBusinesses)
            {
                ds.Businesses.AddBusinessesRow(item.BusinessName, item.BusinessTypeName, item.TownName, item.BusinessCost, item.Lat, item.Lng, item.Id);
            }

            // business types
            foreach (var item in bill.BillProducts)
            {
                ds.Products.AddProductsRow(item.Id, item.ProductTypeName, item.FinalUnitPrice, item.CostPerBusiness);
            }

            // bill
            ds.Bills.AddBillsRow(bill.Id, bill.RegionName, bill.NbrTowns, bill.FinalTotalCost);
                        
            // towns
            foreach (var item in campaign.CampaignTowns)
            {
                var nbrbusiness = 14; // à fixer et trouver une solution pour le calculer
                var townCost = 10.2; // à fixer et trouver une solution pour le calculer
                ds.Towns.AddTownsRow(item.City, nbrbusiness, townCost);
            }            

            return ds;

        }

        public byte[] GenerateCampaignFactureReport(int billId)
        {
            var bill = _billRepository.GetByIdFullData(billId);
            var campaign = _campaignReposotry.GetCampaignFullData(bill.CampaignId);            

            if (bill == null || campaign == null)
                return null;

            var ds = getFactureDataSet(campaign, bill);
            var reportParams = getFactureReportDictionnaryParameters(campaign, bill);

            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "FactureReport.rdlc");
            var localReport = new LocalReport(reportPath);

            var parameters = new List<ReportParameter>();

            // Set report parameters
            foreach (var item in reportParams)
                parameters.Add(new ReportParameter());

            // set report dataset
            localReport.AddDataSource("Towns", ds.Towns);
            localReport.AddDataSource("Products", ds.Products);
            localReport.AddDataSource("Businesses", ds.Businesses);

            // execute report
            var result = localReport.Execute(RenderType.Pdf, extension, reportParams, mimtype);

            // create file as byte [] 
            byte[] file = result.MainStream;

            // return byte[]
            return file;

        }
              
        
        #endregion

    }
}
