using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayStation.Models
{
    internal class Route
    {
        [Column("Route_id")]
        public int Id { get; set; }

        public string? StationId { get; set; }
        public Station? Station { get; set; }

        public int TrainId { get; set; }
        public Train? Train { get; set; }
    }
}
