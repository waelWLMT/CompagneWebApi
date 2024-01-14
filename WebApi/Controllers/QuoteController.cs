using AutoMapper;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public QuoteController(IQuoteService quoteService, ICampaignService campaignService, IBillService billService, IMapper mapper)
        {
            this._quoteService = quoteService;
            this._campaignService = campaignService;
            this._billService = billService;
            this._mapper = mapper;
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

    }
}
