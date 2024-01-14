using AutoMapper;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IFilesService _filesService;
        private readonly IMapper _mapper;

        public PhotoController(IPhotoService photoService, IFilesService filesService, IMapper mapper)
        {
            this._photoService = photoService;
            this._filesService = filesService;
            this._mapper = mapper;
        }
        
        [HttpGet]
        [Route("getByBusinessId/{businessId}")]
        public List<BusinessPhotoReadDto> GetListPhotoByBusiness(int businessId)
        {
            var photosList = _photoService.GetPhotosByBusiness(businessId);
            var result = _mapper.Map<List<BusinessPhotoReadDto>>(photosList);

            return result;

        }
        
        [HttpGet]
        [Route("getByBusinessFilesFolderPath/{campaignId}/{businessId}")]
        public string GetByBusinessFilesFolderPath(int campaignId, int businessId)
        {
            var folderPath = this._filesService.GetBusinessPhotosFolderPath(campaignId,businessId);
            return folderPath; 
        }

    }
}
