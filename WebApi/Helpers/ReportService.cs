using AspNetCore.Reporting;
using AutoMapper;
using Core.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebApi.Dtos;


namespace WebApi.Helpers
{
    public class ReportService : IReportService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IQuoteRepository _quoteRepository;

        public ReportService(IRoleRepository repository, IQuoteRepository quoteRepository)
        {              
            _roleRepository = repository;
            _quoteRepository = quoteRepository;
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

        private void setDevisDataSets(LocalReport devisReport)
        {

        }


        public byte[] CreateReportFile(string pathRdlc)
        {
            try
            {
                var mimeType = "";
                var extension = 1;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var devisReport = new LocalReport(pathRdlc);

                var roles = _roleRepository.GetAll();                
                var ligneDevis = roles;

                var reportParams = getDevisParams();
                
                //setDevisDataSets(devisReport);
                //devisReport.AddDataSource("DS1",ligneDevis);



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
