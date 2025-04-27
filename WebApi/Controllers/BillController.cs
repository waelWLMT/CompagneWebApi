using AutoMapper;
using BL.Services;
using BL.Services.Impl;
using Core.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IMapper _mapper;
        private readonly IBillingReportService _billingReportService;

        public BillController(IBillingReportService billingReportService, IBillService billService, IMapper mapper)
        {
            this._billService = billService;
            this._mapper = mapper;
            _billingReportService = billingReportService;
        }

        [HttpGet]
        [Route("GetBills/{userRoleId}/{customerId}")]
        public List<BillReadDto> GetCustomerBills(int userRoleId, int customerId)
        {
            var bills = userRoleId == 1 ? _billService.GetAllFullData() : _billService.GetAllFullDataByCustomerId(customerId);


            var result = _mapper.Map<List<BillReadDto>>(bills);

            return result;
        }
    
        [HttpGet]
        [Route("CampaignBill/{campaignId}")]
        public BillReadDto CampaignBill(int campaignId)
        {
            var bill = _billService.GetByCampaignId(campaignId);
            var result = _mapper.Map<BillReadDto>(bill);

            return result;
        }

        [HttpGet]
        [Route("GetFactureReport")]
        public IActionResult GetFactureReport(int billId)
        {
            var file = _billingReportService.GenerateBillingReport(billId, true);
            return File(new MemoryStream(file), "application/pdf", "CampaignFactureRpt.pdf");
        }
        
        [HttpGet]
        [Route("getFactureReportByCampagnId")]
        public IActionResult GetFactureReportByCampagnId(int campaignId)
        {
            var file = _billingReportService.GenerateBillingReportByCampagnId(campaignId, true);
            return File(new MemoryStream(file), "application/pdf", "CampaignFactureRpt.pdf");
        }




    }
}
