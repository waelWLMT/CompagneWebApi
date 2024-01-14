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
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            this._roleService = roleService;
            this._mapper = mapper;
        }

        [HttpGet]
        public List<RoleReadDto> getAllRoles()
        {
            var roles = _roleService.GetAll().ToList();

            var result = _mapper.Map<List<RoleReadDto>>(roles);

            return result;

        }
    }
}
