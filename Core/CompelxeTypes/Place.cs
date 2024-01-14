using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{

    [ComplexType]
    public class Place
    {
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        // business Types of specific place
        //public List<string> PlaceTypes { get; set; }
    }
}
