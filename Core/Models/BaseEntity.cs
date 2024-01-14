using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class BaseEntity :IEntity
    {
        [Column(Order = 0)]
        public int Id { get; set; }
        [Column(Order = 1)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column(Order = 2)]
        public DateTime LastModifAt { get; set; } = DateTime.Now;

    }
}
