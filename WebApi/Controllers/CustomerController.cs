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
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ICustomerService _customerService { get; }

        public CustomerController(IMapper mapper, ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }


        [HttpPost]
        public CustomerReadDto AddCustomer(CustomerCreateDto customerCreateDto)
        {
            var customerModel = _mapper.Map<Customer>(customerCreateDto);
            _customerService.Insert(customerModel);
            _customerService.Commit();
            var customerReadDto = _mapper.Map<CustomerReadDto>(customerModel);

            return customerReadDto;
            
        }

        [HttpGet]
        public List<CustomerReadDto> GetAllCustomers()
        {           
            var customerList = _customerService.GetAll().ToList();
            var customerListReadDto = _mapper.Map<List<CustomerReadDto>>(customerList);
            return customerListReadDto;            
        }

        [HttpGet]
        [Route("{id}")]
        public CustomerReadDto GetCustomerById(int id)
        {
            var customerModel = _customerService.GetById(id);
            var customerReadDto = _mapper.Map<CustomerReadDto>(customerModel);

            return customerReadDto;
        }

        [HttpPut]
        [Route("{id}")]
        public CustomerReadDto UpdateCustomer(int id, CustomerReadDto customerReadDto)
        {
            var customerModel = _customerService.GetById(id);
            _mapper.Map(customerReadDto, customerModel);
            _customerService.Commit();
            return customerReadDto;
        }
    }
}
