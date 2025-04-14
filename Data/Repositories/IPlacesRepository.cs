using Core.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IPlacesRepository
    {
        Place BuildPlace(JToken jToken);
        Task<List<Place>> GetPlacesList(Town town, BusinessType businessType);          
        Task<List<Place>> GetListPlacesFromGeoApi(List<string> postalCodes, string placeType);
        Task<List<Place>> GetPlacesFromOverPassApi(List<string> codesPostaux, string tagKey, string tagValue, string pays = "FR");

    }
}
