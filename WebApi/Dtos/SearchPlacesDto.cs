using System.Collections.Generic;

namespace WebApi.Dtos
{
    public class SearchPlacesDto
    {
        public List<string> PostalCodes { get; set; }
        public string PlaceTypeKey { get; set; }
        public string PlaceTypeValue { get; set; }
    }
}
