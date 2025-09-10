using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirVinyContext.Migrations
{
    /// <inheritdoc />
    public partial class BuildDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    NumberOfRecordsOnWishList = table.Column<int>(type: "int", nullable: false),
                    AmountOfCashToSpend = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "VinylRecords",
                columns: table => new
                {
                    VinylRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CatalogNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinylRecords", x => x.VinylRecordId);
                    table.ForeignKey(
                        name: "FK_VinylRecords_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "AmountOfCashToSpend", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "NumberOfRecordsOnWishList" },
                values: new object[,]
                {
                    { 1, 300m, new DateTimeOffset(new DateTime(1981, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), "kevin@kevindockx.com", "Kevin", 1, "Dockx", 10 },
                    { 2, 2000m, new DateTimeOffset(new DateTime(1986, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "sven@someemailprovider.com", "Sven", 1, "Vercauteren", 34 },
                    { 3, 100m, new DateTimeOffset(new DateTime(1977, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "nele@someemailprovider.com", "Nele", 0, "Verheyen", 120 },
                    { 4, 2500m, new DateTimeOffset(new DateTime(1983, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), "nils@someemailprovider.com", "Nils", 1, "Missorten", 23 },
                    { 5, 90m, new DateTimeOffset(new DateTime(1981, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), "tim@someemailprovider.com", "Tim", 1, "Van den Broeck", 19 },
                    { 6, 200m, new DateTimeOffset(new DateTime(1981, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), null, "Kenneth", 1, "Mills", 98 }
                });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Nirvana", "ABC/111", 1, "Nevermind", 1991 },
                    { 2, "Arctic Monkeys", "EUI/111", 1, "AM", 2013 },
                    { 3, "Beatles", "DEI/113", 1, "The White Album", 1968 },
                    { 4, "Beatles", "DPI/123", 1, "Sergeant Pepper's Lonely Hearts Club Band", 1967 },
                    { 5, "Nirvana", "DPI/123", 1, "Bleach", 1989 },
                    { 6, "Leonard Cohen", "PPP/783", 1, "Suzanne", 1967 },
                    { 7, "Marvin Gaye", "MVG/445", 1, "What's Going On", null },
                    { 8, "Nirvana", "ABC/111", 2, "Nevermind", 1991 },
                    { 9, "Cher", "CHE/190", 2, "Closer to the Truth", 2013 },
                    { 10, "The Dandy Warhols", "TDW/516", 3, "Thirteen Tales From Urban Bohemia", null },
                    { 11, "Justin Bieber", "OOP/098", 4, "Baby", null },
                    { 12, "The Prodigy", "NBE/864", 4, "Music for the Jilted Generation", null },
                    { 13, "Anne Clarke", "TII/339", 5, "Our Darkness", null },
                    { 14, "Dead Kennedys", "DKE/864", 5, "Give Me Convenience or Give Me Death", null },
                    { 15, "Sisters of Mercy", "IIE/824", 5, "Temple of Love", null },
                    { 16, "Abba", "TDW/516", 6, "Gimme Gimme Gimme", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VinylRecords_PersonId",
                table: "VinylRecords",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VinylRecords");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
