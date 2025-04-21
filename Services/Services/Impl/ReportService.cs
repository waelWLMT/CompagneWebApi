
using System.Collections.Generic;
using System.IO;
using AspNetCore.Reporting;
using AspNetCore.Reporting.ReportExecutionService;
using Microsoft.AspNetCore.Mvc;
using Reporting;

namespace BL.Services.Impl
{
    public class ReportService : IReportService
    {
        public byte[] GenerateClientReport()
        {
            string mimtype = "";
            int extension = 1;            

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
            /* hedha nzid fel controlleur
            Stream stream = new MemoryStream(file);
            return File(stream, "application/pdf", "testeReport.pdf");
            */

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
