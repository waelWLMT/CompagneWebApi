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
        private Place buildPlaceFromOverPassApiResponse(JToken element)
        {
            var place = new Place();

            // Extract Place Name
            var placeName = element["tags"]?["name"]?.ToString() ?? "Inconnu";
            // Extract Latitude
            double? lat = element["lat"]?.ToObject<double>();
            // Extract Longitude
            double? lon = element["lon"]?.ToObject<double>();

            if (lat.HasValue && lon.HasValue)
            {
                place.Name = placeName;
                place.Lat = lat.Value;
                place.Lng = lon.Value;
                place.PlaceAdresse = buildAdresse(element);

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
                    var place = buildPlaceFromOverPassApiResponse(element);

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

        #endregion




        #region deprecated old code à supprimer
        private string BuildHttpRequestUrl(double lat, double lng, string placeType)
        {
            var url = this._appSettings.GooglePlacesSettings.PlacesApiBaseUrl;

            url += "?location=" + lat;
            url += "," + lng;
            url += "&radius=" + this._appSettings.GooglePlacesSettings.DefaultSearchPlacesRadius;
            url += "&type=" + placeType;
            url += "&keyword=";
            url += "&key=" + this._appSettings.GooglePlacesSettings.GoogleMapPlacesAPIKey;

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
            var url = BuildHttpRequestUrl(town.Lat, town.Lng, businessType.Designation);
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

        public async Task<List<Place>> GetListPlacesFromGeoApi(List<string> postalCodes, string placeType)
        {
            #region new code over pass api

            var codes = new List<string> { "75001", "75002", "75003", "45000" };
            string type = "bar";
            string country = "FR";

            var lieux = await GetPlacesFromOverPassApi(codes, type, country);

            return lieux;


            #endregion

            #region GeoPlacesCode
            //// Créer une instance HttpClient
            //var client = _httpClientFactory.CreateClient();

            //try
            //{
            //    // Liste pour stocker les tâches de recherche
            //    List<Task<string>> tasks = new List<Task<string>>();

            //    // Créer les tâches pour chaque code postal
            //    foreach (var postalCode in postalCodes)
            //    {
            //        // Créer une tâche pour chaque requête HTTP
            //        tasks.Add(GetPlaces(client, postalCode, placeType, _geoApiKey));
            //    }

            //    // Attendre que toutes les requêtes soient terminées
            //    var results = await Task.WhenAll(tasks);



            //    // Liste pour stocker tous les résultats
            //    List<string> allPlaces = new List<string>();

            //    // Traiter les résultats
            //    foreach (var result in results)
            //    {
            //        if (!string.IsNullOrEmpty(result))
            //        {
            //            var places = JsonConvert.DeserializeObject<dynamic>(result);
            //            if (places != null && places.results != null)
            //            {
            //                foreach (var place in places.results)
            //                {
            //                    string placeInfo = $"{place.name}: {place.address}";
            //                    allPlaces.Add(placeInfo);
            //                }
            //            }
            //        }
            //    }

            //    // Afficher tous les résultats après avoir reçu toutes les réponses
            //    if (allPlaces.Any())
            //    {
            //        Console.WriteLine("Liste des boulangeries trouvées:");
            //        foreach (var place in allPlaces)
            //        {
            //            Console.WriteLine($"- {place}");
            //        }
            //    }
            //    //else
            //    //{
            //    //    Console.WriteLine("Aucune boulangerie trouvée dans les codes postaux spécifiés.");
            //    //}

            //    return new List<Place>(allPlaces.Select(p => new Place
            //    {
            //        Name = p.Split(':')[0],
            //        PlaceId = p.Split(':')[1]
            //    }));

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Erreur lors de la recherche: {ex.Message}");
            //    throw ex;

            //}
            //finally
            //{
            //    client.Dispose(); // Libérer les ressources du client HTTP   
            //}

            #endregion
        }

       
        public async Task<List<Place>> GetPlacesFromOverPassApi_OLD(List<string> codesPostaux, string tagKey, string tagValue, string pays = "FR")
        {
            var places = new List<Place>();
            using var client = new HttpClient();

            foreach (var codePostal in codesPostaux)
            {
                string query = $@"
                                [out:json][timeout:25];
                                // Délimitation par pays et code postal
                            area[""name""=""{pays}""]->.country;
                                (
                                 node[""addr:postcode""=""{codePostal}""][""{tagKey}""=""{tagValue}""](area.country);
                                 way[""addr:postcode""=""{codePostal}""][""{tagKey}""=""{tagValue}""](area.country);
                                 relation[""addr:postcode""=""{codePostal}""][""{tagKey}""=""{tagValue}""](area.country);
                            );
                            out center;";

                var response = await client.PostAsync(
                    "https://overpass-api.de/api/interpreter",
                    new StringContent($"data={Uri.EscapeDataString(query)}", Encoding.UTF8, "application/x-www-form-urlencoded")
                );

                var content = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(content);
                JsonElement root = doc.RootElement;

                Console.WriteLine($"\n🔎 Résultats pour {tagKey}={tagValue} dans le code postal {codePostal} ({pays}):");

                foreach (var element in root.GetProperty("elements").EnumerateArray())
                {
                    string name = element.TryGetProperty("tags", out var tags) && tags.TryGetProperty("name", out var nameProp)
                        ? nameProp.GetString()
                        : "Sans nom";

                    string lat = element.TryGetProperty("tags", out var tagsLat) && tags.TryGetProperty("name", out var latProp)
                        ? latProp.GetString()
                        : "Sans nom";

                    string lng = element.TryGetProperty("tags", out var tagsLng) && tags.TryGetProperty("name", out var lngProp)
                        ? lngProp.GetString()
                        : "Sans nom";


                    Console.WriteLine($"• Type: {element.GetProperty("type")} | Nom: {name}");
                    Console.WriteLine($"• Type: {element.GetProperty("type")} | Nom: {lat}");
                    Console.WriteLine($"• Type: {element.GetProperty("type")} | Nom: {lng}");

                    places.Add(new Place() { Name = name, Lat = Convert.ToDouble(lat), Lng = Convert.ToDouble(lng) });
                }

            }

            return places;
        }

        

        #endregion


      
    }
}
