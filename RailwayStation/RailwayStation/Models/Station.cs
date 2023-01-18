using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayStation.Models
{
    internal class Station
    {
        [Required]
        [Key]
        public string? Address { get; set; } = null!;
        [MaxLength(75)]
        public string? Station_name { get; set; }
        [Required]
        public int Worker_amount { get; set; } = 0;
        public int Passenger_amount { get; set; } = 0;

        public List<Worker> Workers { get; set; } = new();

        public List<Waybil> WaybilsTo { get; set; } = new();

        public List<Waybil> WaybilsFrom { get; set; } = new();

        public List<Train> TrainsTo { get; set; } = new();
        public List<Train> TrainsFrom { get; set; } = new();

        public List<Route> Routes { get; set; } = new();
    }
}
