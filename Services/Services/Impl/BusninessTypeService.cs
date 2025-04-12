using BL.ServicePattern;
using Core.Models;
using Data.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Impl
{
    public class BusninessTypeService : ServicePattern<BusinessType>, IBusninessTypeService
    {
        private readonly IBusninessTypeRepository _businessTypeRepo;
        private readonly HttpClient _httpClient;

        public BusninessTypeService(IBusninessTypeRepository businessTypeRepo, HttpClient httpClient): base(businessTypeRepo) 
        {
            this._businessTypeRepo = businessTypeRepo;
            _httpClient = httpClient;

        }

        public ICollection<BusinessType> GetActivatedBusinessTypes()
        {
            return _businessTypeRepo.GetActivatedBusinessTypes();
        }

        public async Task<ICollection<BusinessType>> GetByPostalCodesAndType(List<string> postalCodes, string mapCode)
        {

            var baseUrl = "testApiUrl";
            var response = await _httpClient.GetAsync(baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var businessTypes = JsonConvert.DeserializeObject<ICollection<BusinessType>>(jsonResponse);
                return businessTypes;
            }
            else
            {
                // Handle error response
                throw new Exception("Error fetching data from API");
            }




            return null;
        }
    }
}
