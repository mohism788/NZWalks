using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDiffandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("12001abb-fe0b-46de-ae53-d2f82845a91b"), "Medium" },
                    { new Guid("7ceb9dea-ffc8-4ecd-8379-3614f598844c"), "Hard" },
                    { new Guid("a1e439f5-1367-4c1f-a9dd-f5bc481993c8"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("05761056-cdbf-46e4-9dc4-eae8b2d4e749"), "BOP", "Bay of Plenty", "https://images.mapsofworld.com/newzealand/bay-of-plenty-map.jpg" },
                    { new Guid("790e2b23-b023-40aa-a99d-72b2e6a6cdf9"), "GIS", "Gisborne", "https://www.exploretheeastcape.co.nz/uploads/6/1/5/1/61514749/7-gisborne_orig.jpg" },
                    { new Guid("7d4b3021-4ed4-4b47-9cb5-e9353d991539"), "AKL", "Auckland", "https://upload.wikimedia.org/wikipedia/commons/d/d1/Auckland_Region_map_EN.png" },
                    { new Guid("d4ffb7b8-579f-4dca-a9a1-4d5d0b55197c"), "WKO", "Waikato", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5b/Waikato_Region_location_in_New_Zealand.svg/250px-Waikato_Region_location_in_New_Zealand.svg.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("12001abb-fe0b-46de-ae53-d2f82845a91b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7ceb9dea-ffc8-4ecd-8379-3614f598844c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a1e439f5-1367-4c1f-a9dd-f5bc481993c8"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("05761056-cdbf-46e4-9dc4-eae8b2d4e749"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("790e2b23-b023-40aa-a99d-72b2e6a6cdf9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7d4b3021-4ed4-4b47-9cb5-e9353d991539"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d4ffb7b8-579f-4dca-a9a1-4d5d0b55197c"));
        }
    }
}
