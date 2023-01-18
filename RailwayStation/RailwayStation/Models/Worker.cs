using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayStation.Models
{
    internal class Worker : Human
    {
        [Column("Worker_id")]
        public int Id { get; set; }
     
        [Required]
        public string? Position { get; set; }
        public int Salary { get; set; }
        [Required]

        public string? StationAddress { get; set; }
        public Station? Station { get; set; }
    }
}
