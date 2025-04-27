using AutoMapper;
using BL.Services;
using BL.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly ICampaignService _campaignService;
        private readonly IBillService _billService;
        private readonly IBillingReportService _billingReportService;
        private readonly IMapper _mapper;

        public QuoteController(IBillingReportService billingReportService, IQuoteService quoteService, ICampaignService campaignService, IBillService billService, IMapper mapper)
        {
            this._quoteService = quoteService;
            this._campaignService = campaignService;
            this._billService = billService;
            this._mapper = mapper;
            this._billingReportService = billingReportService;
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

        [HttpGet]
        [Route("getDevisCampaigneReportByCampaignId")]
        public IActionResult GetDevisCampaigneReportByCampaignId(int campaignId)
        {
            var file = _billingReportService.GenerateBillingReportByCampagnId(campaignId, false);
            return File(new MemoryStream(file), "application/pdf", "CampaignDevisRpt.pdf");
        }


        [HttpGet]
        [Route("getDevisCampaigneReport")]
        public IActionResult GetDevisCampaigneReport(int devisId)
        {
            var file = _billingReportService.GenerateBillingReport(devisId, false);
            return File(new MemoryStream(file), "application/pdf", "CampaignDevisRpt.pdf");
        }
      
    }
}
