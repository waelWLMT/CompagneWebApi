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
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;

        public RegionController(IRegionService regionService, IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;
        }        

        [HttpGet]        
        public List<RegionReadDto> getAllRegions()
        {
            // get only activated regions
            var listRegions = _regionService.GetListActivatedRegions();
            var result = _mapper.Map<List<RegionReadDto>>(listRegions);
            return result;
        }


    }
}
