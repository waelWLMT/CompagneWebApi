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
    public class TownController : ControllerBase
    {
        private readonly ITownService _townService;
        private readonly IMapper _mapper;

        public TownController(ITownService townService, IMapper mapper)
        {
            _townService = townService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("fullEntity/{fullEntity}")]
        public List<TownReadDto> getAllTowns(bool fullEntity)
        {
            var townsList = fullEntity ? _townService.GetFullTowns() :  _townService.GetAll().ToList();
            var result = _mapper.Map<List<TownReadDto>>(townsList);

            return result;
        }

        [HttpGet]
        [Route("TownsByRegion/{regionId}/{fullEntity}")]        
        public List<TownReadDto> getTownByRegion(int regionId, bool fullEntity)
        {
            var listTowns = _townService.GetTownsByRegion(regionId, fullEntity);
            var result = _mapper.Map<List<TownReadDto>>(listTowns);

            return result;
        }
        


    }
}
