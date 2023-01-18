using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayStation.Models
{
    internal class Train
    {
        [Column("Train_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        
        public int Railway_JourneyId { get; set; }
        public Railway_journey? Railway_Journey { get; set; }

        public string? StationFromId { get; set; }
        public Station? Start_point { get; set; }

        public string? StationToId { get; set; }
        public Station? Destination { get; set; }

        public List<Route> Routes { get; set; } = new();
    }
}
