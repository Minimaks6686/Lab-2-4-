using RailwayStation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using static RailwayStation.Commands;







var commandsBuilder = new DbContextOptionsBuilder<RailwayStationContext>();
var commands = commandsBuilder.Options;
using (RailwayStationContext context = new (commands))
{

    //foreach (var station in context.Stations)
    //    Console.WriteLine(station.Address);

    //Console.WriteLine(new String('+', 20));

    //foreach (var worker in context.Workers)
    //    Console.WriteLine(worker.Name + ' ' + worker.Last_name);

    //Console.WriteLine(new String('+', 20));

    //Navigation(commands);

    ////Console.WriteLine(new String('+', 20));

    ////foreach (var passenger in context.Passenger)
    ////    Console.WriteLine(passenger.Name + ' ' + passenger.Last_name + ' ' + passenger.Personal_document);

    Inquiry(commands);

}