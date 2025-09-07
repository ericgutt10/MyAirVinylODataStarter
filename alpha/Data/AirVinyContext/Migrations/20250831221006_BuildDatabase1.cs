using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirVinyContext.Migrations
{
    /// <inheritdoc />
    public partial class BuildDatabase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "RecordStores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                table: "PressingDetails",
                columns: new[] { "PressingDetailId", "Description", "Grams", "Inches" },
                values: new object[,]
                {
                    { 1, "Audiophile LP", 180, 12 },
                    { 2, "Regular LP", 140, 12 },
                    { 3, "Audiophile Single", 50, 7 },
                    { 4, "Regular Single", 40, 7 }
                });

            migrationBuilder.InsertData(
                table: "RecordStores",
                columns: new[] { "RecordStoreId", "Discriminator", "Name", "Tags", "StoreAddress_City", "StoreAddress_Country", "StoreAddress_PostalCode", "StoreAddress_Street" },
                values: new object[] { 1, "RecordStore", "All Your Music Needs", "[\"Rock\",\"Pop\",\"Indie\",\"Alternative\"]", "Antwerp", "Belgium", "2000", "25, Fluffy Road" });

            migrationBuilder.InsertData(
                table: "RecordStores",
                columns: new[] { "RecordStoreId", "StoreAddress_City", "StoreAddress_Country", "StoreAddress_PostalCode", "StoreAddress_Street", "Discriminator", "Name", "Specialization", "Tags" },
                values: new object[,]
                {
                    { 2, "Antwerp", "Belgium", "2000", "1, Main Street", "SpecializedRecordStore", "Indie Records, Inc", "Indie", "[\"Rock\",\"Indie\",\"Alternative\"]" },
                    { 3, "Antwerp", "Belgium", "2000", "5, Big Street", "SpecializedRecordStore", "Rock Records, Inc", "Rock", "[\"Rock\",\"Pop\"]" }
                });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatedByPersonId", "RecordStoreId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, 4 },
                    { 2, 2, 1, 4 },
                    { 3, 3, 1, 4 },
                    { 4, 1, 2, 5 },
                    { 5, 2, 2, 4 },
                    { 6, 3, 3, 5 },
                    { 7, 2, 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Nirvana", "ABC/111", 1, 1, "Nevermind", 1991 },
                    { 2, "Arctic Monkeys", "EUI/111", 1, 2, "AM", 2013 },
                    { 3, "Beatles", "DEI/113", 1, 2, "The White Album", 1968 },
                    { 4, "Beatles", "DPI/123", 1, 2, "Sergeant Pepper's Lonely Hearts Club Band", 1967 },
                    { 5, "Nirvana", "DPI/123", 1, 1, "Bleach", 1989 },
                    { 6, "Leonard Cohen", "PPP/783", 1, 3, "Suzanne", 1967 },
                    { 7, "Marvin Gaye", "MVG/445", 1, 1, "What's Going On", null },
                    { 8, "Nirvana", "ABC/111", 2, 1, "Nevermind", 1991 },
                    { 9, "Cher", "CHE/190", 2, 2, "Closer to the Truth", 2013 },
                    { 10, "The Dandy Warhols", "TDW/516", 3, 2, "Thirteen Tales From Urban Bohemia", null },
                    { 11, "Justin Bieber", "OOP/098", 4, 3, "Baby", null },
                    { 12, "The Prodigy", "NBE/864", 4, 2, "Music for the Jilted Generation", null },
                    { 13, "Anne Clarke", "TII/339", 5, 3, "Our Darkness", null },
                    { 14, "Dead Kennedys", "DKE/864", 5, 2, "Give Me Convenience or Give Me Death", null },
                    { 15, "Sisters of Mercy", "IIE/824", 5, 4, "Temple of Love", null },
                    { 16, "Abba", "TDW/516", 6, 4, "Gimme Gimme Gimme", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "VinylRecords",
                keyColumn: "VinylRecordId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PressingDetails",
                keyColumn: "PressingDetailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PressingDetails",
                keyColumn: "PressingDetailId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PressingDetails",
                keyColumn: "PressingDetailId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PressingDetails",
                keyColumn: "PressingDetailId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RecordStores",
                keyColumn: "RecordStoreId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RecordStores",
                keyColumn: "RecordStoreId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RecordStores",
                keyColumn: "RecordStoreId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "RecordStores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
