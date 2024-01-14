using AutoMapper;
using BL.Services;
using Core.Models;
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
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IMapper _mapper;
        public BillController(IBillService billService, IMapper mapper)
        {
            this._billService = billService;
            this._mapper = mapper;
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

    }
}
