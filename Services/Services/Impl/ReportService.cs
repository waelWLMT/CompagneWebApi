
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
using Reporting;
using Reporting.RptDataSets.Devis;
using static Reporting.RptDataSets.Devis.DevisDataSet;

namespace BL.Services.Impl
{
    public class ReportService : IReportService
    {
        private readonly IQuoteRepository _quoteReposotry;
        private readonly ICampaignRepository _campaignReposotry;

        private string mimtype = "";
        private int extension = 1;

        public ReportService(IQuoteRepository quoteReposotry, ICampaignRepository campaignReposotry)
        {
            _quoteReposotry = quoteReposotry;
            _campaignReposotry = campaignReposotry;
        }
        
        private Dictionary<string, string> getDevisReportDictionnaryParameters(Campaign campaign, Quote quote, string logoPath)
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

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "Img","logo.jpg");

            if (quote == null || campaign == null)
                return null;

            var ds = getDevisDataSet(campaign, quote);
            var reportParams = getDevisReportDictionnaryParameters(campaign,quote, logoPath);       
                
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

        public byte[] GenerateClientReport()
        {
            var _reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ClientReport.rdlc");
            var localReport = new LocalReport(_reportPath);

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt.Columns.Add("Nom");
            dt.Columns.Add("Email");

            dt.Rows.Add("Wael", "testEmailWael");
            dt.Rows.Add("Aymen", "testEmailAymen");

            localReport.AddDataSource("Clients", dt);

            //Geração do arquivo
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = localReport.Execute(RenderType.Pdf, extension, null);
            byte[] file = result.MainStream;

            return file;
            


        }


    }
}
