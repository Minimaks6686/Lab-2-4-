using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RailwayStation.Models
{
    internal class Passenger : Human
    {
        public int Age { get; set; }
        [Required]
        public string? Personal_document { get; set; }

        public virtual List<Waybil> Waybils { get; set; } = new ();
    }
}
