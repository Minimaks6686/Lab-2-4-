using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayStation.Models
{
    internal class Railway_journey
    {
        [Column("Railway_journey_id")]
        public int Id { get; set; }

        public List<Train>? Trains { get; set; } = new ();
        public int Car_amount { get; set; }
        public DateTime Start_point_date { get; set; }
        public DateTime Destination_date { get; set; }
        
        public List<Car>? Cars { get; set; } = new ();

        public List<Railway_Journey_Car>? Railway_Journeys_Cars { get; set; } = new ();
    }
}
