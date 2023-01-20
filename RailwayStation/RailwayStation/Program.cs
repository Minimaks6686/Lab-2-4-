using RailwayStation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using static RailwayStation.Commands;
using RailwayStation.Models;


var commandsBuilder = new DbContextOptionsBuilder<RailwayStationContext>();
var commands = commandsBuilder.Options;


//Асинхронно додати 99 станцій у 3 потоки
async Task AddStationsAsync(int k)
{
    using RailwayStationContext context = new(commands);
    for (int i = k; i < k + 33; i++)
    {
        await context.Stations.AddAsync(new Station { Address = "Address " + i, Station_name = "Station_name " + i });
    }
    await context.SaveChangesAsync();
}

using RailwayStationContext context = new(commands);
var Add1 = AddStationsAsync(0);
var Add2 = AddStationsAsync(33);
var Add3 = AddStationsAsync(66);
await Task.WhenAll(Add1, Add2, Add3);
