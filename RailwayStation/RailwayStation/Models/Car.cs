using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayStation.Models
{
    internal class Car
    {
        [Column("Car_id")]
        public int Id { get; set; }
        public int Number { get; set; }
        [Required]
        public string Type { get; set; }
        public int Seat_amount { get; set; } = 54;
        public int Seat_occupied { get; set; } = 0;
        
        public List<Railway_journey> ?Railway_Journeys { get; set; } = new ();

        public List<Railway_Journey_Car> ?Railway_Journeys_Cars { get; set; } = new ();
    }
}
