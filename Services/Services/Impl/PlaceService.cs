using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Data.Repositories;


namespace BL.Services.Impl
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlacesRepository _placeRepository;
        private readonly IBusninessTypeRepository _businessTypeRepository;

        public PlaceService(IPlacesRepository placeRepository, IBusninessTypeRepository businessTypeRepository)
        {
            _placeRepository = placeRepository;
            _businessTypeRepository = businessTypeRepository;
        }
        public async Task<List<Place>> GetPlacesList(List<string> postalCodes, string placeTypeKey, string placeTypeValue)
        {
            return await _placeRepository.GetPlacesFromOverPassApi(postalCodes, placeTypeKey, placeTypeValue);
        }

        public async Task<List<Place>> GetPlacesList(string postalCode, string placeTypeIds)
        {
            var ids = placeTypeIds
                .Split(',')
                .Select(x => Convert.ToInt32(x.Trim()))
                .ToList();

            var businessTypes = _businessTypeRepository
                .GetAll()
                .Where(x =>
                ids.Contains(x.Id))
                .ToHashSet();

            var places = await _placeRepository.GetPlacesFromOverPassApi(postalCode, businessTypes);


            return places;




        }
    }
}
