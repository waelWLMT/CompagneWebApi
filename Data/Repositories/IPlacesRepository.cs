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
        List<Place> GetPlacesList(Town town, BusinessType businessType);       
    }
}
