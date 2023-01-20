using Microsoft.EntityFrameworkCore;
using RailwayStation.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailwayStation
{
    public class Protect
    {

        //Найпопулярніші маршрути по вихідним дням
        static public void IndependentWork(DbContextOptions commands)
        {
            using (RailwayStationContext context = new(commands))
            {
                var trains = context.Trains.Join(context.Railway_Journeys
                    .Where(rj => rj.Start_point_date == new DateTime(2022, 10, 14) || rj.Start_point_date == new DateTime(2022, 10, 15))
                    , t => t.Railway_JourneyId
                    , r => r.Id,
                    (t, r) => new { t.Number, r.Id });

                var topTrains = from w in context.Waybils
                                group w by w.Train_number into g
                                select new
                                {
                                    number = g.Key,
                                    count = g.Count()
                                };

                var GRailwayJourney = trains.Join(topTrains, t => t.Number, tt => tt.number,
                    (t, tt) => new { t.Id, t.Number, tt.count }).OrderBy(g => g.count);

                foreach (var top in GRailwayJourney)
                    Console.WriteLine(top.Id + " " + top.count);
            }
        }
    }
}
