using Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Impl
{
    /// <summary>
    /// this class implements IPlacesRepository
    /// this class is used only when google Map places License is available
    /// this class is used also in production envirements
    /// </summary>
    public class PlacesRepository : IPlacesRepository
    {
       
        private double _searchRadius;
        private string _apiKey;
        private string _baseApiUrl = "";

        public PlacesRepository(IConfiguration configuration)
        {            
            this._searchRadius = Convert.ToDouble(configuration.GetSection("DefaultSearchPlacesRadius").Value);
            this._apiKey = configuration.GetSection("GoogleMapPlacesAPIKey").Value;
            this._baseApiUrl = configuration.GetSection("PlacesApiBaseUrl").Value;
        }

        private string BuildHttpRequestUrl(double lat, double lng, string placeType)
        {
            var url = this._baseApiUrl;

            url += "?location=" + lat;
            url += "," + lng;
            url += "&radius=" + this._searchRadius;
            url += "&type=" + placeType;
            url += "&keyword=";
            url += "&key=" + this._apiKey;

            return url;
        }
       
        public Place BuildPlace(JToken jToken)
        {
            var place = new Place();

            place.PlaceId = jToken.SelectToken("place_id").Value<string>();
            place.Name = jToken.SelectToken("name").Value<string>();
            place.Lat = jToken.SelectToken("geometry").SelectToken("location").SelectToken("lat").Value<double>();
            place.Lng = jToken.SelectToken("geometry").SelectToken("location").SelectToken("lng").Value<double>();

            return place;
        }
      
        public async Task<List<Place>> GetPlacesList(Town town, BusinessType businessType)
        {
            var places = new List<Place>();

            // make http get request
            var httpClient = HttpClientFactory.Create();
            var url = BuildHttpRequestUrl(town.Lat, town.Lng, businessType.MapCode);
            var httpResponseMessage = await httpClient.GetAsync(url);


            // extract places from response
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var content = httpResponseMessage.Content;
                //var data = content.ReadAsStringAsync();

                var data = content.ReadAsStringAsync();

                var jObject = JObject.Parse(data.Result);
                var jArray = jObject.SelectToken("results");

                foreach (var item in jArray)
                    places.Add(BuildPlace(item));
            }

            return places;
        }

        List<Place> IPlacesRepository.GetPlacesList(Town town, BusinessType businessType)
        {
            throw new NotImplementedException();
        }
    }
}
