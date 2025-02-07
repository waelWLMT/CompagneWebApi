using AspNetCore.Reporting;
using AutoMapper;
using Core.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebApi.Dtos;


namespace WebApi.Helpers
{
    public class ReportService : IReportService
    {
        private readonly IBillRepository _billRepository;        

        public ReportService(IBillRepository repository)
        {
            _billRepository = repository;            
        }

        private Dictionary<string,string> getDevisParams()
        {
            var reportParameters = new Dictionary<string,string>();
            
            reportParameters.Add("titre", "Devis");
            reportParameters.Add("clientName", "Orange Fr");
            reportParameters.Add("regionName", "Paris");
            reportParameters.Add("regionCode", "75");

            return reportParameters;

        }

        private void setFactureDataSet(LocalReport devisReport)
        {
            var bills = _billRepository.GetAll().Take(1);
            devisReport.AddDataSource("Factures", bills);

        }


        public byte[] CreateReportFile(string pathRdlc)
        {
            try
            {
                var mimeType = "";
                var extension = 1;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var devisReport = new LocalReport(pathRdlc);                
                var reportParams = getDevisParams();
                setFactureDataSet(devisReport);                
                var result = devisReport.Execute(RenderType.Pdf, extension, reportParams, mimeType);

                return result.MainStream;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

        }
    }
}
