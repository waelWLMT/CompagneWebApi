using AutoMapper;
using BL.Services;
using Core.Models;
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
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public BillController(IReportService reportService, IBillService billService, IMapper mapper)
        {
            this._billService = billService;
            this._mapper = mapper;
            _reportService = reportService;
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
            var file = _reportService.GenerateCampaignFactureReport(billId);
            if (file == null)
                return null;

            Stream stream = new MemoryStream(file);
            return File(stream, "application/pdf", "CampaignFactureRpt.pdf");
        }


    }
}
