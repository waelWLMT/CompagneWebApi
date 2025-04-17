using Core.CompelxeTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Dtos
{
    public class PlaceReadDto
    {
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        [NotMapped]
        public Address PlaceAdresse { get; set; }
    }
}
