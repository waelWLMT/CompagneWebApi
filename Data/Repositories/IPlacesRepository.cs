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
        Task<List<Place>> GetPlacesFromOverPassApi(List<string> codesPostaux, string tagKey, string tagValue, string pays = "FR");
        Task<List<Place>> GetPlacesFromOverPassApi(string postalCode, HashSet<BusinessType> placeTypes, string pays = "FR");
    }
}
