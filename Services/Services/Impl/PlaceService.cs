using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Data.Repositories;

namespace BL.Services.Impl
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlacesRepository _placeRepository;

        public PlaceService(IPlacesRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }
        public async Task<List<Place>> GetPlacesList(List<string> postalCodes, string placeTypeKey, string placeTypeValue)
        {
            return await _placeRepository.GetPlacesFromOverPassApi(postalCodes, placeTypeKey, placeTypeValue);
            //return await _placeRepository.GetListPlacesFromGeoApi(postalCodes, placeType);
        }
    }
}
