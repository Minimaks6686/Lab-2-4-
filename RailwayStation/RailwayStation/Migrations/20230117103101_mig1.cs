using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RailwayStation.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Carid = table.Column<int>(name: "Car_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Type = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Seatamount = table.Column<int>(name: "Seat_amount", type: "int", nullable: false),
                    Seatoccupied = table.Column<int>(name: "Seat_occupied", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Carid);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lastname = table.Column<string>(name: "Last_name", type: "nvarchar(450)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Personaldocument = table.Column<string>(name: "Personal_document", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => new { x.Name, x.Lastname });
                    table.CheckConstraint("Age", "Age > 1 AND Age < 120");
                });

            migrationBuilder.CreateTable(
                name: "Railway_journey",
                columns: table => new
                {
                    Railwayjourneyid = table.Column<int>(name: "Railway_journey_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Caramount = table.Column<int>(name: "Car_amount", type: "int", nullable: false),
                    Startpointdate = table.Column<DateTime>(name: "Start_point_date", type: "datetime2", nullable: false),
                    Destinationdate = table.Column<DateTime>(name: "Destination_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Railway_journey", x => x.Railwayjourneyid);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Stationname = table.Column<string>(name: "Station_name", type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Workeramount = table.Column<int>(name: "Worker_amount", type: "int", nullable: false),
                    Passengeramount = table.Column<int>(name: "Passenger_amount", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Address);
                    table.UniqueConstraint("AK_Stations_Station_name", x => x.Stationname);
                });

            migrationBuilder.CreateTable(
                name: "Railway_Journey_Car",
                columns: table => new
                {
                    RailwayJourneyId = table.Column<int>(name: "Railway_JourneyId", type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Railway_Journey_Car", x => new { x.CarId, x.RailwayJourneyId });
                    table.ForeignKey(
                        name: "FK_Railway_Journey_Car_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Car_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Railway_Journey_Car_Railway_journey_Railway_JourneyId",
                        column: x => x.RailwayJourneyId,
                        principalTable: "Railway_journey",
                        principalColumn: "Railway_journey_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Train",
                columns: table => new
                {
                    Trainid = table.Column<int>(name: "Train_id", type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RailwayJourneyId = table.Column<int>(name: "Railway_JourneyId", type: "int", nullable: false),
                    StationFromId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StationToId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train", x => x.Trainid);
                    table.ForeignKey(
                        name: "FK_Train_Railway_journey_Railway_JourneyId",
                        column: x => x.RailwayJourneyId,
                        principalTable: "Railway_journey",
                        principalColumn: "Railway_journey_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Train_Stations_StationFromId",
                        column: x => x.StationFromId,
                        principalTable: "Stations",
                        principalColumn: "Address");
                    table.ForeignKey(
                        name: "FK_Train_Stations_StationToId",
                        column: x => x.StationToId,
                        principalTable: "Stations",
                        principalColumn: "Address");
                });

            migrationBuilder.CreateTable(
                name: "Waybils",
                columns: table => new
                {
                    Waybilid = table.Column<int>(name: "Waybil_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PassengerLastName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Trainnumber = table.Column<int>(name: "Train_number", type: "int", nullable: false),
                    Carnumber = table.Column<int>(name: "Car_number", type: "int", nullable: false),
                    Cartype = table.Column<string>(name: "Car_type", type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Seatnumber = table.Column<int>(name: "Seat_number", type: "int", nullable: false),
                    StationFromId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StationToId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waybils", x => x.Waybilid);
                    table.ForeignKey(
                        name: "FK_Waybils_Passenger_PassengerName_PassengerLastName",
                        columns: x => new { x.PassengerName, x.PassengerLastName },
                        principalTable: "Passenger",
                        principalColumns: new[] { "Name", "Last_name" });
                    table.ForeignKey(
                        name: "FK_Waybils_Stations_StationFromId",
                        column: x => x.StationFromId,
                        principalTable: "Stations",
                        principalColumn: "Address");
                    table.ForeignKey(
                        name: "FK_Waybils_Stations_StationToId",
                        column: x => x.StationToId,
                        principalTable: "Stations",
                        principalColumn: "Address");
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Workerid = table.Column<int>(name: "Worker_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    StationAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lastname = table.Column<string>(name: "Last_name", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Workerid);
                    table.UniqueConstraint("AK_Workers_Name_Last_name", x => new { x.Name, x.Lastname });
                    table.ForeignKey(
                        name: "FK_Workers_Stations_StationAddress",
                        column: x => x.StationAddress,
                        principalTable: "Stations",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Routeid = table.Column<int>(name: "Route_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TrainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Routeid);
                    table.ForeignKey(
                        name: "FK_Routes_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Address");
                    table.ForeignKey(
                        name: "FK_Routes_Train_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "Train_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Address", "Passenger_amount", "Station_name", "Worker_amount" },
                values: new object[,]
                {
                    { "м. Київ, Вацлава Гавела 4", 0, "Західна", 0 },
                    { "м. Київ, Вацлава Гавела 5", 0, "Східна", 0 },
                    { "м. Київ, Вацлава Гавела 6", 0, "Північна", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Railway_Journey_Car_Railway_JourneyId",
                table: "Railway_Journey_Car",
                column: "Railway_JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StationId",
                table: "Routes",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TrainId",
                table: "Routes",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Train_Railway_JourneyId",
                table: "Train",
                column: "Railway_JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Train_StationFromId",
                table: "Train",
                column: "StationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Train_StationToId",
                table: "Train",
                column: "StationToId");

            migrationBuilder.CreateIndex(
                name: "IX_Waybils_PassengerName_PassengerLastName",
                table: "Waybils",
                columns: new[] { "PassengerName", "PassengerLastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Waybils_StationFromId",
                table: "Waybils",
                column: "StationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Waybils_StationToId",
                table: "Waybils",
                column: "StationToId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_StationAddress",
                table: "Workers",
                column: "StationAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Railway_Journey_Car");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Waybils");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Train");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "Railway_journey");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
