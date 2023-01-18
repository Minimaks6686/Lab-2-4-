using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayStation.Models
{
    internal class Waybil
    {
        [Column("Waybil_id")]
        public int Id { get; set; }
        
        public string? PassengerName { get; set; }
        public string? PassengerLastName { get; set; }
        public virtual Passenger? Passenger { get; set; }

        public int Train_number { get; set; }
        public int Car_number { get; set; }
        [Required]
        [MaxLength(3)]
        public string? Car_type { get; set; }
        public int Seat_number { get; set; }

        public string? StationFromId { get; set; }
        public Station? Personal_start_point { get; set; }

        public string? StationToId { get; set; }
        public Station? Personal_destination { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Type { get; set; }
    }
}
