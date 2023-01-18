using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayStation.Models
{
    internal class Railway_Journey_Car
    {
        public int Railway_JourneyId { get; set; }
        public Railway_journey? Railway_Journey { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}
