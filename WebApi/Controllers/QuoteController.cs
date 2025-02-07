using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AutoMapper;
using BL.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly ICampaignService _campaignService;
        private readonly IBillService _billService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IReportService _reportService;

        public QuoteController(IQuoteService quoteService, ICampaignService campaignService, IBillService billService, IMapper mapper, IHostingEnvironment hostingEnvironment, IReportService reportService)
        {
            this._quoteService = quoteService;
            this._campaignService = campaignService;
            this._billService = billService;
            this._mapper = mapper;
            this._hostingEnvironment = hostingEnvironment;
            this._reportService = reportService;
        }

        [HttpGet]
        [Route("{userRoleId}/{customerId}")]
        public List<QuoteReadDto> GetDevisByRoleUser(int userRoleId, int customerId)
        {
            var quotes = this._quoteService.GetQuotesByRoleUser(userRoleId, customerId);
            var result = _mapper.Map<List<QuoteReadDto>>(quotes);

            return result;
        }
       
        [HttpGet]
        [Route("GetReportById")]
        public object GetDevisReport()
        {
            
            var path = _hostingEnvironment.ContentRootPath + "\\Reports\\Report.rdlc";
            var byteRes = _reportService.CreateReportFile(path);


            return File(byteRes, System.Net.Mime.MediaTypeNames.Application.Octet, "ReportName.pdf");

        }

        [HttpGet]
        [Route("GenerateCampaignQuote/{campaignId}")]
        public int GenerateCampaignQuote(int campaignId)
        {
            this._quoteService.CreateDevis(campaignId);
            var campaign = this._campaignService.GetCampaignByIdFullData(campaignId);
            var billId = this._billService.GenerateBill(campaign);

            return billId;
        }

        [HttpGet]
        [Route("{devisId}")]
        public QuoteReadDto GetDevisById(int devisId)
        {
            var devis = this._quoteService.GetQuoteFullDataById(devisId);
            var result = this._mapper.Map<QuoteReadDto>(devis);

            return result;
        }

    }
}
