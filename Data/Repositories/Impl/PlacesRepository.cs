using Core.CompelxeTypes;
using Core.Models;
using Core.Utils.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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

        public AppSettings _appSettings { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public PlacesRepository(IHttpClientFactory httpclientFactory, IOptions<AppSettings> appSettings)
        {
            _httpClientFactory = httpclientFactory;
            _appSettings = appSettings.Value;
        }

        #region overpass api good code
        private string makeCountryFilter()
        {
            return $"area[\"ISO3166-1\"=\"{_appSettings.CountrySettings.CountryCode}\"]->.country;";
        }
        private string makePostalCodesFilter(List<string> postalCodes)
        {
            return string.Join("\n", postalCodes.Select((pc, i) =>
                $"area[\"postal_code\"=\"{pc}\"](area.country)->.a{i};"));
        }
        private string makeQueryBody(List<string> postalCodes, string tagKey, string tagValue)
        {
            return string.Join("\n", postalCodes.Select((pc, i) => $@"
                    node[""{tagKey}""=""{tagValue}""](area.a{i});
                    way[""{tagKey}""=""{tagValue}""](area.a{i});
                    relation[""{tagKey}""=""{tagValue}""](area.a{i});
            "));
        }
        private string buildOverPassQueryString(string countryFilter, string areaDeclarations, string queryBody)
        {
            return $@"
                        [out:json][timeout:25];
                        {countryFilter}
                        {areaDeclarations}
            (
                        {queryBody}
            );
            out center;
            ";
        }
        private Address buildAdresse(JToken element)
        {
            Address address = new Address();

            // Extraction des informations de l'adresse
            address.HouseNumber = element["tags"]?["addr:housenumber"]?.ToString().Trim() ?? "N/A";
            address.Street = element["tags"]?["addr:street"]?.ToString().Trim() ?? "Inconnu";
            address.TownName = element["tags"]?["addr:city"]?.ToString().Trim() ?? "Inconnu";
            address.PostalCode = element["tags"]?["addr:postcode"]?.ToString().Trim() ?? "Inconnu";
            address.CountryName = _appSettings.CountrySettings.CountryName.Trim();

            return address;

        }
        private Place buildPlaceFromOverPassApiResponse(JToken element, HashSet<BusinessType> tags)
        {
            var place = new Place();
            
            var tagKey = "";   // Place Type key == Business type tagkeyCode
            var tagValue = ""; // Place Type key == Business type tagValueCode

            foreach (var tag in tags)
            {
                if (element["tags"]?[tag.TagKeyCode] != null && element["tags"]?[tag.TagKeyCode].ToString().Trim() == tag.TagValueCode) {
                    tagKey = tag.TagKeyCode;
                    tagValue = tag.TagValueCode;
                    break;
                }
            }

            // Extract Place Name
            var placeName = element["tags"]?["name"]?.ToString() ?? "Inconnu";
            // Extract Latitude
            double? lat = element["lat"]?.ToObject<double>();
            // Extract Longitude
            double? lon = element["lon"]?.ToObject<double>();

            if (lat.HasValue && lon.HasValue && !string.IsNullOrEmpty(tagKey) && !string.IsNullOrEmpty(tagValue))
            {
                place.PlaceId = element["id"]?.ToString() ?? "Inconnu";
                place.Name = placeName;
                place.Lat = lat.Value;
                place.Lng = lon.Value;
                place.PlaceAdresse = buildAdresse(element);
                place.TagKeyCode = tagKey;
                place.TagValueCode = tagValue;

                return place;
            }            

            return null;

        }
        public async Task<List<Place>> GetPlacesFromOverPassApi(List<string> postalCodes, string tagKey, string tagValue, string countryCode = "FR")
        {

            var places = new List<Place>();
            var overpassUrl = _appSettings.OverPassApiSettings.OverPassUrl;
            var countryFilter = makeCountryFilter();  // Zone pays (ex: FR pour France)
            var areaDeclarations = makePostalCodesFilter(postalCodes); // Codes postaux filtrés dans le pays
            var queryBody = makeQueryBody(postalCodes, tagKey, tagValue); // Génération dynamique des requêtes par zone


            var overpassQuery = buildOverPassQueryString(countryFilter, areaDeclarations, queryBody); // build overpass query

            var client = _httpClientFactory.CreateClient();
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", overpassQuery) });

            HttpResponseMessage response = await client.PostAsync(overpassUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                JObject result = JObject.Parse(json);

                foreach (var element in result["elements"])
                {
                    var place = buildPlaceFromOverPassApiResponse(element, new HashSet<BusinessType> { new BusinessType { TagKeyCode = tagKey, TagValueCode = tagValue } });

                    if (place != null)
                        places.Add(place);
                }
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }

            return places;

        }
        public async Task<List<Place>> GetPlacesFromOverPassApi(string postalCode, HashSet<BusinessType> tags, string pays = "FR")
        {
            // cette methode doit chercher la liste des lieux en se basant sur un code postale et une liste des tags

            var places = new List<Place>();
            var overpassUrl = _appSettings.OverPassApiSettings.OverPassUrl;
            var query = makeMyQuery(postalCode, tags, pays);

            using var client = new HttpClient();
            var content = new StringContent("data=" + Uri.EscapeDataString(query), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync(overpassUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                JObject result = JObject.Parse(json);

                foreach (var element in result["elements"])
                {
                    var place = buildPlaceFromOverPassApiResponse(element, tags);

                    if (place != null)
                        places.Add(place);
                }
            }
            else
            {
                throw new Exception($"Erreur Overpass API: {response.StatusCode}");
            }

            return places;


        }
        private string makeMyQuery(string postalCode, HashSet<BusinessType> tags, string pays)
        {
            // Requête dynamique avec filtre pays + code postal
            var query = "[out:json][timeout:25];\n";

            // Définir l'aire du pays (FR = France, admin_level=2)
            query += $"area[\"ISO3166-1\"=\"{pays}\"][admin_level=2]->.country;\n";

            // Définir l'aire du code postal *dans* ce pays
            query += $"area[\"postal_code\"=\"{postalCode}\"][boundary=postal_code](area.country)->.searchArea;\n";

            // Ouverture du bloc de requête
            query += "(\n";

            foreach (var tag in tags)
            {
                query += $"  node[\"{tag.TagKeyCode}\"=\"{tag.TagValueCode}\"](area.searchArea);\n";
            }

            query += ");\nout body;\n>;\nout skel qt;";

            return query;
        }

        #endregion





    }
}
