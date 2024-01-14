using AutoMapper;
using BL.Services;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApi.Builders;
using WebApi.Dtos;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICampaignService _campaignService;
        private readonly IBillService _billService;
        public IFilesService _filesService { get; }
        private readonly IPhotoService _photoService;

        public CampaignController(ICampaignService campaignService, IMapper mapper, IBillService billService, IFilesService filesService, IPhotoService photoService)
        {
            _mapper = mapper;
            _campaignService = campaignService;
            _billService = billService;
            _filesService = filesService;
            _photoService = photoService;
        }

        #region Campaign Managment

        [HttpGet]
        [Route("CloseCampaign/{campaignId}/{userId}")]
        public int CloseCampaign(int campaignId, int userId)
        {
            var campaign = _campaignService.CloseCampaign(campaignId, userId);
            // la generation des factures est automatique juste aprés la validation de devis            
            return campaign.Id;
        }

        [HttpGet]
        [Route("LaunchRealization/{campaignId}/{userId}")]
        public CampaignReadDto LaunchRealization(int campaignId, int userId)
        {
            var campaign = _campaignService.LaunchCampaignRealization(campaignId, userId);
            var result = _mapper.Map<CampaignReadDto>(campaign);

            return result;
        }

        [HttpPost]
        [Route("SearchCampaign/")]
        public List<CampaignReadDto> SearchCampaign(SearchCampaignCreteriaDto creteria)
        {
            var campaigns = _campaignService.SearchCampaignByCreteria(creteria.StartDate, creteria.EndDate, creteria.ClientId, creteria.RegionId, creteria.Towns, creteria.BusinessTypes);
            var result = _mapper.Map<List<CampaignReadDto>>(campaigns);

            return result;
        }

        [HttpGet]
        [Route("DuplicateCampagn/{campaignId}/{userId}")]
        public int DuplicateCampaign(int campaignId, int userId)
        {
            var nveCampaignId = this._campaignService.DuplicateCampaign(campaignId, userId);
            return nveCampaignId;
        }

        [HttpPost]
        public int CreateCompaignStepOne(CampaignCreateDto campaignCreateDto)
        {
            var campaign = _mapper.Map<Campaign>(campaignCreateDto);
            var campaignId = _campaignService.CreateCampaign(campaign, campaignCreateDto.RegionId, campaignCreateDto.TownsIds, campaignCreateDto.BusinessTypesIds, campaignCreateDto.ProductTypeIds, campaignCreateDto.CustomerId);

            return campaignId;
        }

        [HttpDelete]
        [Route("{id}")]
        public bool DeleteCampaignById(int id)
        {
            _campaignService.Delete(id);
            _campaignService.Commit();

            return true;
        }

        [HttpGet]
        [Route("{id}")]
        public CampaignReadDto GetCampaignById(int id)
        {
            var campaign = _campaignService.GetCampaignByIdFullData(id);

            var campaignReadDto = _mapper.Map<CampaignReadDto>(campaign);

            return campaignReadDto;
        }

        [HttpGet]
        [Route("GetAllCampaigns")]
        public List<CampaignReadDto> GetAllCampaigns()
        {
            var campaigns = _campaignService.GetAllCampaigns();
            var result = _mapper.Map<List<CampaignReadDto>>(campaigns);

            return result;
        }

        #endregion

        #region Campaign Towns Managment

        [HttpGet]
        [Route("getCampaignTownMap/{campaignId}")]
        public List<DetailsCampaignTownReadDto> GetCampaignTownMap(int campaignId)
        {

            var detailesTownsList = _campaignService.GetListDetailsCampaignTowns(campaignId);
            var result = _mapper.Map<List<DetailsCampaignTownReadDto>>(detailesTownsList);

            return result;
        }

        [HttpDelete]
        [Route("deleteCampaignTown/{campaignId}/{townId}")]
        public CampaignReadDto DeleteCampaignTown(int campaignId, int townId)
        {
            var campaign = _campaignService.DeleteCampaignTown(campaignId, townId);
            var result = _mapper.Map<CampaignReadDto>(campaign);

            return result;
        }

        [HttpGet]
        [Route("addCampaignTown/{campaignId}/{townId}")]

        public CampaignReadDto AddCampaignTown(int campaignId, int townId)
        {
            var campaign = _campaignService.AddCampaignTown(campaignId, townId);
            var result = _mapper.Map<CampaignReadDto>(campaign);

            return result;
        }



        #endregion

        #region Campaign Products Managment

        [HttpDelete]
        [Route("deleteCamapaignProduct/{campaignId}/{productTypeId}")]
        public CampaignReadDto DeleteCampaignProduct(int campaignId, int productTypeId)
        {
            var campaign = _campaignService.DeleteCampaignProduct(campaignId, productTypeId);
            var campaignDto = _mapper.Map<CampaignReadDto>(campaign);

            return campaignDto;
        }

        [HttpPut]
        [Route("updateCamapaignProduct/{campaignId}/")]
        public CampaignReadDto UpdateCampaignProduct(int campaignId, ProductUpdateDto productUpdateDto)
        {

            var campaign = _campaignService.UpdateCampaignProduct(campaignId, productUpdateDto.ProductTypeId, productUpdateDto.FinalNbrProductPerBusiness, productUpdateDto.FinalPrice);
            var result = _mapper.Map<CampaignReadDto>(campaign);

            return result;
        }

        [HttpGet]
        [Route("addCamapaignProduct/{campaignId}/{productTypeId}")]
        public CampaignReadDto AddCampaignProduct(int campaignId, int productTypeId)
        {
            var campaign = _campaignService.AddCampaignProduct(campaignId, productTypeId);

            var result = _mapper.Map<CampaignReadDto>(campaign);

            return result;
        }

        #endregion

        #region Campaign Businesses and Business Types Managment

        [HttpPost, DisableRequestSizeLimit]
        [Route("UpdateCampaignBusinessState")]
        public async Task<IActionResult> UpdateCampaignBusinessState(int campaignId, int businessCampaignId, int oldStateId, int newStateId, int userModifId)
        {
            try
            {
                var business = this._campaignService.UpdateCampaignBusinessState(campaignId, newStateId, userModifId, businessCampaignId);

                if (business != null)
                {                    
                    var formCollection = await Request.ReadFormAsync();
                    var files = formCollection.Files;
                    var path = _filesService.CreateCampaignBusinessFilesDirectoryIfNotExist(campaignId, business.CampaignBusinessId); 
                    var listPhotoNames = _filesService.UploadListFiles(files.ToList(), path);
                    _photoService.AddListPhotos(business, listPhotoNames);
                }

                return Ok() ;

            }catch(Exception)
            {
                return StatusCode(500);
            }            
        }

        [HttpDelete]
        [Route("deleteCampaignBusinessType/{campaignId}/{BusinessTypeMapCode}")]
        public CampaignReadDto DeleteCampaignBusinessType(int campaignId, string BusinessTypeMapCode)
        {
            var campaign = _campaignService.DeleteCampaignBusinessType(campaignId, BusinessTypeMapCode);
            var campaignDto = _mapper.Map<CampaignReadDto>(campaign);

            return campaignDto;
        }

        [HttpGet]
        [Route("addCampaignBusinessType/{campaignId}/{businessTypeMapCode}")]
        public CampaignReadDto AddCampaignBusinessType(int campaignId, string businessTypeMapCode)
        {
            var campaign = _campaignService.AddCampaignBusinessType(campaignId, businessTypeMapCode);
            var result = _mapper.Map<CampaignReadDto>(campaign);

            return result;
        }

        #endregion

        #region Campaign Global Params Managment

        [HttpPut]
        [Route("updateCampaignGlobalParams/{campaignId}/")]
        public CampaignReadDto UpdateCampaignGlobalParams(int campaignId, CampaignGlobalParmsUpdateDto globalParmsUpdateDto)
        {
            var campaignModif = new Campaign()
            {
                Title = globalParmsUpdateDto.Title,
                Goal = globalParmsUpdateDto.Goal,
                ForecastBudget = globalParmsUpdateDto.ForecastBudget,
                ExecutionDate = globalParmsUpdateDto.ExecutionDate,
                PenetraionRate = globalParmsUpdateDto.PenetrationRate,
                Description = globalParmsUpdateDto.Description
            };


            var campaign = _campaignService.UpdateCampaignGlobalParameters(campaignId, campaignModif);
            var result = _mapper.Map<CampaignReadDto>(campaign);

            return result;
        }

        #endregion

        
    }
}
