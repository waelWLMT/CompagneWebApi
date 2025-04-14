using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace BL.Services
{
    public interface IPlaceService
    {
        Task<List<Place>> GetPlacesList(List<string> postalCodes, string placeTypeKey, string placeTypeValue);

    }
}
