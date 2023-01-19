using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RailwayStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RailwayStation
{
    internal class Commands
    {
        static public void Add(DbContextOptions commands)
        {
            using RailwayStationContext context = new(commands);
            Worker worker1 = new Worker() { Position = "Boss", Salary = 25000, StationAddress = "м. Київ, Вацлава Гавела 4", Name = "Max", Last_name = "Tailor" };
            Worker worker2 = new Worker() { Position = "Boss", Salary = 25000, StationAddress = "м. Київ, Вацлава Гавела 5", Name = "Vova", Last_name = "Call" };
            Worker worker3 = new Worker() { Position = "Boss", Salary = 25000, StationAddress = "м. Київ, Вацлава Гавела 4", Name = "Masha", Last_name = "Ramsy" };
            context.Workers.AddRange(worker1, worker2, worker3);

            Passenger passenger1 = new() { Age = 18, Name = "Max", Last_name = "Tailor", Personal_document = "pasport"};
            Passenger passenger2 = new() { Age = 18, Name = "July", Last_name = "Kim", Personal_document = "Student doc" };
            context.Passenger.AddRange(passenger1, passenger2);

            Waybil waybil1 = new() { PassengerName = "Max", PassengerLastName = "Tailor", Train_number = 1, Car_number = 1, Car_type = "lux", Seat_number = 1, StationFromId = "м. Київ, Вацлава Гавела 4", StationToId = "м. Київ, Вацлава Гавела 5", Type = "def" };
            Waybil waybil2 = new() { PassengerName = "July", PassengerLastName = "Kim", Train_number = 2, Car_number = 2, Car_type = "eco", Seat_number = 1, StationFromId = "м. Київ, Вацлава Гавела 5", StationToId = "м. Київ, Вацлава Гавела 4", Type = "st" };
            context.Waybils.AddRange(waybil1, waybil2);
            context.SaveChanges();
        }

        static public void Update(DbContextOptions commands)
        {
            using RailwayStationContext context = new(commands);
            var worker = context.Workers.Where(w => w.Name == "Max" && w.Last_name == "Tailor").FirstOrDefault();
                if (worker != null)
                {
                    worker.StationAddress = "м. Київ, Вацлава Гавела 6";
                    context.SaveChanges();
                }
        }

        static public void Delete(DbContextOptions commands)
        {
            using RailwayStationContext context = new(commands);
            var worker = context.Workers.Where(w => w.Name == "Masha" && w.Last_name == "Ramsy").FirstOrDefault();
            if (worker != null)
            {
                context.RemoveRange(worker);
                context.SaveChanges();
            } 
        }

        static public void Navigation(DbContextOptions commands)
        {
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var stations = context.Stations.Include(s => s.Workers).ToList();

                foreach (var station in stations)
                    foreach (var worker in station.Workers)
                        Console.WriteLine($"{station.Station_name} - {worker.Name} {worker.Last_name}");
            }
            Console.WriteLine(new String('+', 20));
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var stations = context.Stations.ToList();
                foreach (var station in stations)
                {
                    context.Workers.Where(w => w.StationAddress == station.Address).Load();
                    Console.WriteLine($"{station.Station_name}:");
                    if (station.Workers.Count != 0)
                        foreach (var worker in station.Workers)
                            Console.WriteLine($"{worker.Name} {worker.Last_name}");

                    else
                        Console.WriteLine("Nobody");
                }
            }
            //Console.WriteLine(new String('+', 20));
            //using (RailwayStationContext context = new RailwayStationContext(commands))
            //{
            //    var passengers = context.Passenger.Where(p => p.Name == "Max").ToList();
            //    foreach (var passenger in passengers)
            //        foreach (var waybil in passenger.Waybils)
            //            Console.WriteLine($"{passenger.Name} {passenger.Last_name} - {waybil.Type}");
            //}
        }

        static public void Inquiry(DbContextOptions commands)
        {
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var workers = context.Workers.Select(w => new
                {
                    Name = w.Name,
                    Last_name = w.Last_name
                });
                var passengers = context.Passenger.Select(p => new
                {
                    Name = p.Name,
                    Last_name = p.Last_name
                });

                var peoples = workers
                .Union(passengers);
                foreach (var people in peoples)
                    Console.WriteLine(people.Name + ' ' + people.Last_name);
            }

            Console.WriteLine(new String('+', 20));
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var workers = context.Workers.Select(w => new
                {
                    Name = w.Name,
                    Last_name = w.Last_name
                });
                var passengers = context.Passenger.Select(p => new
                {
                    Name = p.Name,
                    Last_name = p.Last_name
                });

                var peoples = workers
                .Except(passengers);
                foreach (var people in peoples)
                    Console.WriteLine(people.Name + ' ' + people.Last_name);
            }

            Console.WriteLine(new String('+', 20));
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var workers = context.Workers.Select(w => new
                {
                    Name = w.Name,
                    Last_name = w.Last_name
                });
                var passengers = context.Passenger.Select(p => new
                {
                    Name = p.Name,
                    Last_name = p.Last_name
                });

                var peoples = workers
                .Intersect(passengers);
                foreach (var people in peoples)
                    Console.WriteLine(people.Name + ' ' + people.Last_name);
            }

            Console.WriteLine(new String('+', 20));
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var workersSt = context.Workers.Join(context.Stations,
                    w => w.StationAddress,
                    s => s.Address,
                    (w, s) => new
                    {
                        Name = w.Name,
                        Station = s.Station_name,
                        Last_Name = w.Last_name
                    });
                foreach (var w in workersSt)
                    Console.WriteLine($"{w.Name} {w.Last_Name} - {w.Station}");
            }

            Console.WriteLine(new String('+', 20));
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var groups = from w in context.Workers
                             group w by w.Station!.Address into g
                             select new
                             {
                                 g.Key,
                                 Count = g.Count()
                             };
                foreach (var group in groups)
                {
                    Console.WriteLine($"{group.Key} - {group.Count}");
                }
            }

            Console.WriteLine(new String('+', 20));
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var workersSt = context.Workers.Join(context.Stations,
                    w => w.StationAddress,
                    s => s.Address,
                    (w, s) => new
                    {
                        Station = s.Station_name
                    });
                foreach (var w in workersSt.Distinct())
                    Console.WriteLine(w.Station);
            }

            Console.WriteLine(new String('+', 20));
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                if (context.Workers.Any(u => u.Station!.Station_name == "Західна"))
                    Console.WriteLine("Any - OK!");
                else
                    Console.WriteLine("Any - No OK!");

                Console.WriteLine(new String('-', 20));

                if (context.Workers.All(u => u.Station!.Station_name == "Західна"))
                    Console.WriteLine("All - OK!");
                else
                    Console.WriteLine("All - No OK!");

                Console.WriteLine(new String('-', 20));

                int minAge = context.Passenger.Min(u => u.Age);

                int maxAge = context.Passenger.Max(u => u.Age);

                double avgAge = context.Passenger.Average(p => p.Age);

                Console.WriteLine(minAge);

                Console.WriteLine(new String('-', 20));

                Console.WriteLine(maxAge);

                Console.WriteLine(new String('-', 20));

                Console.WriteLine(avgAge);

                Console.WriteLine(new String('-', 20));

                int sum = context.Workers.Where(w => w.Station!.Station_name == "Західна")
                       .Sum(w => w.Salary);
                Console.WriteLine(sum);
            }
        }

        static public void Tracking(DbContextOptions commands)
        {
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                var pas1 = context.Passenger.FirstOrDefault();
                var pas2 = context.Passenger.AsNoTracking().FirstOrDefault();

                if (pas1 != null && pas2 != null)
                {
                    Console.WriteLine($"Before pas1: {pas1.Age}   pas2: {pas2.Age}");

                    pas1.Age = 25;

                    Console.WriteLine($"After pas1: {pas1.Age}   pas2: {pas2.Age}");
                }

            }
        }
        static public void StoredFunc(DbContextOptions commands)
        {
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                SqlParameter param = new SqlParameter("@age", 19);
                var passengers = context.Passenger.FromSqlRaw("SELECT * FROM GetPassAge (@age)", param).ToList();
                foreach (var passenger in passengers)
                    Console.WriteLine($"{passenger.Name} - {passenger.Age}");
            }
        }

        static public void StoredProc(DbContextOptions commands)
        {
            using (RailwayStationContext context = new RailwayStationContext(commands))
            {
                SqlParameter param = new("@name", "Західна");
                var workers = context.Workers.FromSqlRaw("GetWorkersbyStation @name", param).ToList();
                foreach (var worker in workers)
                    Console.WriteLine($"{worker.Name} {worker.Last_name}");
            }
        }
    }
}
