using Core.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Impl
{

    /// <summary>
    /// this class implements IPlaces Repository
    /// this class is used for developpement mode and test instead of using google Map places Api
    /// when we get google map places Api license this class is not used any more
    /// </summary>
    public class MockPlacesRepository : IPlacesRepository
    {
        private ICollection<Place> _parisRestaurants;
        private ICollection<Place> _parisSalleDeSport;

        private ICollection<Place> _montreuilRestaurants;
        private ICollection<Place> _montreuilSalleDeSport;

        public MockPlacesRepository()
        {
            // Paris

            #region  List des restaurant à Paris
            this._parisRestaurants = new List<Place>();

            var place1 = new Place() { Lat = 48.870502, Lng = 2.304897, Name = "Masha", PlaceId = "Rest1" };
            var place2 = new Place() { Lat = 48.8426, Lng = 2.3289, Name = "Cubana", PlaceId = "Rest2" };
            var place3 = new Place() { Lat = 48.8688, Lng = 2.3007, Name = "Le Cinq", PlaceId = "Rest3" };
            var place4 = new Place() { Lat = 48.8557, Lng = 2.3170, Name = "Arpège", PlaceId = "Rest3" };
            var place5 = new Place() { Lat = 48.8912, Lng = 2.3471, Name = "Masha", PlaceId = "Rest4" };

            var place6 = new Place() { Lat = 48.8553, Lng = 2.3644, Name = "L'Ambroisie", PlaceId = "Rest5" };
            var place7 = new Place() { Lat = 48.37948, Lng = -4.4895421, Name = "Taillevent", PlaceId = "Rest6" };
            var place8 = new Place() { Lat = 48.8498, Lng = 2.3549, Name = "La Tour d'Argent", PlaceId = "Rest7" };
            var place9 = new Place() { Lat = 48.8529, Lng = 2.3899, Name = "Masha", PlaceId = "Rest8" };
            var place10 = new Place() { Lat = 48.8685, Lng = 2.3701, Name = "Vantre", PlaceId = "Rest9" };


            _parisRestaurants.Add(place1); _parisRestaurants.Add(place2); _parisRestaurants.Add(place3);
            _parisRestaurants.Add(place4); _parisRestaurants.Add(place5); _parisRestaurants.Add(place6);
            _parisRestaurants.Add(place7); _parisRestaurants.Add(place8); _parisRestaurants.Add(place9);
            _parisRestaurants.Add(place10);

            #endregion

            #region List des salle de sport à paris

            this._parisSalleDeSport = new List<Place>();

            var placeS1 = new Place() { Lat = 48.8699, Lng = 2.3292, Name = "Yoga Village Paris", PlaceId = "Sport1" };
            var placeS2 = new Place() { Lat = 48.8739, Lng = 2.3651, Name = "Battling Club Paris 10", PlaceId = "Sport2" };
            var placeS3 = new Place() { Lat = 48.8589, Lng = 2.3494, Name = "Cercle de la Forme Châtelet - salle de sport paris 4e", PlaceId = "Sport2" };
            var placeS4 = new Place() { Lat = 48.8760, Lng = 2.3279, Name = "The Circle of Health", PlaceId = "Sport3" };
            var placeS5 = new Place() { Lat = 48.8699, Lng = 2.3264, Name = "La Salle de Sport with Reebok", PlaceId = "Sport4" };

            _parisSalleDeSport.Add(placeS1); _parisSalleDeSport.Add(placeS2); _parisSalleDeSport.Add(placeS3);
            _parisSalleDeSport.Add(placeS4); _parisSalleDeSport.Add(placeS5);

            #endregion

            // Montreuil

            #region  List des restaurant à Montreuil
            this._montreuilRestaurants = new List<Place>();

            var place21 = new Place() { Lat = 48.6183, Lng = 2.4255, Name = "Sinalya ", PlaceId = "Rest1" };
            var place22 = new Place() { Lat = 48.8534, Lng = 2.4347, Name = "Le Jardin de Montreuil", PlaceId = "Rest2" };
            var place23 = new Place() { Lat = 48.8638, Lng = 2.4332, Name = "Villa 9 Trois", PlaceId = "Rest3" };
            var place24 = new Place() { Lat = 48.8600, Lng = 2.4364, Name = "Le Rue Parmentier", PlaceId = "Rest3" };
            var place25 = new Place() { Lat = 48.8576, Lng = 2.4332, Name = "Bistrot du Métro Montreuil", PlaceId = "Rest4" };

            var place26 = new Place() { Lat = 48.8594, Lng = 2.4399, Name = "La Maison Montreuil", PlaceId = "Rest5" };
            var place27 = new Place() { Lat = 45.6667, Lng = 1.9167, Name = "Le Plateau des Mille Vaches", PlaceId = "Rest6" };
            var place28 = new Place() { Lat = 48.8709, Lng = 2.3318, Name = "Restaurant de la Paix", PlaceId = "Rest7" };
            var place29 = new Place() { Lat = 48.8498, Lng = 2.3786, Name = "Le Gévaudan", PlaceId = "Rest8" };
            var place210 = new Place() { Lat = 48.8639, Lng = 2.4439, Name = "Mojo Montreuil", PlaceId = "Rest9" };


            _montreuilRestaurants.Add(place21); _montreuilRestaurants.Add(place22); _montreuilRestaurants.Add(place23);
            _montreuilRestaurants.Add(place24); _montreuilRestaurants.Add(place25); _montreuilRestaurants.Add(place26);
            _montreuilRestaurants.Add(place27); _montreuilRestaurants.Add(place28); _montreuilRestaurants.Add(place29);
            _montreuilRestaurants.Add(place210);

            #endregion

            #region  List des salle de sport à Montreuil

            this._montreuilSalleDeSport = new List<Place>();

            var placeSL21 = new Place() { Lat = 48.8567, Lng = 2.4343, Name = "Fitness Park Montreuil ", PlaceId = "Rest1" };
            var placeSL22 = new Place() { Lat = 48.8494, Lng = 2.4189, Name = "Curves Salle de sport pour Femmes | Vincennes Saint Mandé Montreuil", PlaceId = "Rest2" };
            var placeSL23 = new Place() { Lat = 48.8701, Lng = 2.4671, Name = "On Air Fitness Montreuil", PlaceId = "Rest3" };
            var placeSL24 = new Place() { Lat = 48.8683, Lng = 2.4442, Name = "District Training Zone", PlaceId = "Rest3" };
            var placeSL25 = new Place() { Lat = 48.8333, Lng = 2.2397, Name = "Gymnase Paul Bert", PlaceId = "Rest4" };

            var placeSL26 = new Place() { Lat = 48.8594, Lng = 2.2397, Name = "Centre Sportif Arthur Ashe", PlaceId = "Rest5" };
            var placeSL27 = new Place() { Lat = 48.8628, Lng = 2.4448, Name = "Gymnase Diderot 2", PlaceId = "Rest6" };
            var placeSL28 = new Place() { Lat = 48.8536, Lng = 2.4207, Name = "Elan Sportif de Montreuil", PlaceId = "Rest7" };
            var placeSL29 = new Place() { Lat = 48.8697, Lng = 2.4241, Name = "Fitness Park Bagnolet", PlaceId = "Rest8" };
            var placeSL210 = new Place() { Lat = 48.8563, Lng = 2.4343, Name = "Complexe Sportif René Doriant", PlaceId = "Rest9" };

            _montreuilSalleDeSport.Add(placeSL21); _montreuilSalleDeSport.Add(placeSL22); _montreuilSalleDeSport.Add(placeSL23);
            _montreuilSalleDeSport.Add(placeSL24); _montreuilSalleDeSport.Add(placeSL25); _montreuilSalleDeSport.Add(placeSL26);
            _montreuilSalleDeSport.Add(placeSL27); _montreuilSalleDeSport.Add(placeSL28); _montreuilSalleDeSport.Add(placeSL29);
            _montreuilSalleDeSport.Add(placeSL210);

            #endregion

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

        public List<Place> GetPlacesList(Town town, BusinessType businessType)
        {
            if (town.City.ToUpper().Contains("PARIS"))            
                return businessType.Id == 38 ? (List<Place>)_parisSalleDeSport : (List<Place>)_parisRestaurants;            
            else
                return businessType.Id == 38 ? (List<Place>) _montreuilSalleDeSport : (List<Place>)_montreuilRestaurants;                     
        }
    }
}
