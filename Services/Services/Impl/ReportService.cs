
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
           
            if(quote == null || campaign == null)
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

            /*
            var ds = new ClientDataSet();
            ds.DataSetName = "ClientDataSet";
            ds.Clients.Clear();              
            

            ds.Clients.AddClientsRow("Wael", "wael.mrabet@gmail.com");
            ds.Clients.AddClientsRow("Aymen", "aymen.mrabet@gmail.com");
            ds.Clients.AddClientsRow("Hamid", "hamid.benaissa@gmail.com");

            localReport.AddDataSource(ds.DataSetName, ds.Clients);
            */
            /*
            // Create data set
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt.Columns.Add("Nom", typeof(string));
            dt.Columns.Add("Email", typeof(string));

            dt.Rows.Add("Mrabet", "wael.mrabet@gmail.com");
            dt.Rows.Add("Ben Aissa", "hamid.hamid.benaissa@gmail.com");
            
            localReport.AddDataSource("ClientDataSet", dt);
            */

            /* add report parameters
            //Parametros do relatório
            var reportParams = new Dictionary<string, string>();
            //reportParams.Add("Key1", "value1");
            //reportParams.Add("Key2", "value2");
            if (reportParams != null && reportParams.Count > 0)// if you use parameter in report
            {
                List<ReportParameter> reportparameter = new List<ReportParameter>();
                foreach (var record in reportParams)
                {
                    reportparameter.Add(new ReportParameter());
                }

            }
            */

            //Geração do arquivo
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = localReport.Execute(RenderType.Pdf, extension, null);
            byte[] file = result.MainStream;

            return file;


            /*
            var clientDs = new ClientDataSet();
            clientDs.DataSetName = "ClientDataSet";

            clientDs.Clients.AddClientsRow("Wael", "wael.mrabet@gmail.com");
            clientDs.Clients.AddClientsRow("Aymen", "aymen.mrabet@gmail.com");
            clientDs.Clients.AddClientsRow("Hamid", "hamid.benaissa@gmail.com");

            var localReport = new Microsoft.Reporting.WebForms.LocalReport();
            localReport.ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ClientReport.rdlc");
            
            var clientRptDataSource = new ReportDataSource("ClientDataSet");
            clientRptDataSource.Value = clientDs;
            localReport.DataSources.Add(clientRptDataSource);
            
            byte[] reportBytes = localReport.Render("PDF");

            return reportBytes;
            */




        }


    }
}
