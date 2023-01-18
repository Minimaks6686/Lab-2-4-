using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RailwayStation.Models;

namespace RailwayStation
{
    internal class RailwayStationContext : DbContext
    {

        public DbSet<Station> Stations { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Route> Routes { get; set; } = null!;
        public DbSet<Waybil> Waybils { get; set; } = null!;
        public DbSet<Worker> Workers { get; set; } = null!;
        public DbSet<Passenger> Passenger { get; set; } = null!;

        public RailwayStationContext(DbContextOptions commands) : base(commands)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

                                                    //Fluent API

            modelBuilder.Entity<Passenger>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Passenger>().HasKey(p => new { p.Name, p.Last_name });
            modelBuilder.Entity<Passenger>().ToTable(p => p.HasCheckConstraint("Age", "Age > 1 AND Age < 120"));

            modelBuilder.Entity<Station>().HasAlternateKey(s => s.Station_name);

            modelBuilder.Entity<Worker>().HasAlternateKey(w => new { w.Name, w.Last_name });

            modelBuilder.Entity<Car>().Property(c => c.Type).HasMaxLength(3);
            modelBuilder.Entity<Car>().Property(c => c.Number).HasDefaultValue(1);




                                                        //ЗВ'ЯЗКИ

            modelBuilder.Entity<Car>().HasMany(c => c.Railway_Journeys).WithMany(rj=>rj.Cars).UsingEntity<Railway_Journey_Car>(
            rjc => rjc
                .HasOne(pt => pt.Railway_Journey)
                .WithMany(t => t.Railway_Journeys_Cars)
                .HasForeignKey(pt => pt.Railway_JourneyId).OnDelete(DeleteBehavior.Restrict),
            rjc => rjc
                .HasOne(pt => pt.Car)
                .WithMany(p => p.Railway_Journeys_Cars)
                .HasForeignKey(pt => pt.CarId).OnDelete(DeleteBehavior.Restrict),
            rjc =>
            {
                rjc.HasKey(t => new { t.CarId, t.Railway_JourneyId });
                rjc.ToTable("Railway_Journey_Car");
            });

            modelBuilder.Entity<Worker>().HasOne(w => w.Station).WithMany(s => s.Workers).HasForeignKey(si => si.StationAddress);

            modelBuilder.Entity<Waybil>().HasOne(w => w.Passenger).WithMany(p => p.Waybils).HasForeignKey(wi => new { wi.PassengerName, wi.PassengerLastName });
            modelBuilder.Entity<Waybil>().HasOne(w => w.Personal_start_point).WithMany(s => s.WaybilsFrom).HasForeignKey(w => w.StationFromId);
            modelBuilder.Entity<Waybil>().HasOne(w => w.Personal_destination).WithMany(s => s.WaybilsTo).HasForeignKey(w => w.StationToId);

            modelBuilder.Entity<Train>().HasOne(t => t.Railway_Journey).WithMany(rj => rj.Trains).HasForeignKey(t => t.Railway_JourneyId);
            modelBuilder.Entity<Train>().HasOne(t => t.Start_point).WithMany(s => s.TrainsFrom).HasForeignKey(t => t.StationFromId);
            modelBuilder.Entity<Train>().HasOne(t => t.Destination).WithMany(s => s.TrainsTo).HasForeignKey(t => t.StationToId);

            modelBuilder.Entity<Route>().HasOne(r => r.Station).WithMany(s => s.Routes).HasForeignKey(r => r.StationId);
            modelBuilder.Entity<Route>().HasOne(r => r.Train).WithMany(t => t.Routes).HasForeignKey(r => r.TrainId);

                                    //ЗАПОВНЕННЯ БАЗИ ПОЧАТКОВИМИ ДАНИМИ

            modelBuilder.Entity<Station>().HasData(
                    new Station { Address = "м. Київ, Вацлава Гавела 4", Station_name = "Західна" },
                    new Station { Address = "м. Київ, Вацлава Гавела 5", Station_name = "Східна" },
                    new Station { Address = "м. Київ, Вацлава Гавела 6", Station_name = "Північна" }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connect = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json").Build()
                              .GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connect!);

           // optionsBuilder
           //.UseLazyLoadingProxies()        
           //.UseSqlServer(connect!);


        }
    }
}
