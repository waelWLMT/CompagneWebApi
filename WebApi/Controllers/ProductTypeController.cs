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
    public class ProductTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IProductTypeService _productTypeService;
        public ProductTypeController(IMapper mapper, IProductTypeService productTypeService)
        {
            _mapper = mapper;
            _productTypeService = productTypeService;
        }

        [HttpPost]
        public ProductTypeReadDto AddProductType(ProductTypeCreateDto productTypeCreateDto)
        {
            var productTypeModel = _mapper.Map<ProductType>(productTypeCreateDto);

            _productTypeService.Insert(productTypeModel);
            _productTypeService.Commit();

            var productTypeReadDto = _mapper.Map<ProductTypeReadDto>(productTypeModel);

            return productTypeReadDto;
        }

        [HttpGet]
        public List<ProductTypeReadDto> GetAllProductTypes()
        {
            var productTypes = _productTypeService.GetAll().ToList();
            var result = _mapper.Map<List<ProductTypeReadDto>>(productTypes);

            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public ProductTypeReadDto GetProductTypesById(int id)
        {
            var productType = _productTypeService.GetById(id);
            var result = _mapper.Map<ProductTypeReadDto>(productType);

            return result;
        }

        [HttpPut]
        [Route("{id}")]
        public ProductTypeReadDto GetProductTypesById(int id, ProductTypeReadDto productTypeReadDto)
        {

            var productType = _productTypeService.GetById(id);
            productType = _mapper.Map<ProductType>(productTypeReadDto);
            _productTypeService.Commit();
           
            return productTypeReadDto;
        }
    }
}
