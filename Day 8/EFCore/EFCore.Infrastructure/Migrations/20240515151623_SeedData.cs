using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore.Services.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("017cd214-ac14-45a5-9aff-b130d4d9f3d0"));

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("597d9a32-e7ba-4ad5-ad03-0e7dd0db626d"));

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("8b65c196-e84a-4a06-90f7-5525cc58f91f"));

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("bf3ab168-64b9-44b2-80d1-1b1055363899"));

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6b8dd2d4-24b3-47ae-ba70-6224b3b86c0c"), "HR" },
                    { new Guid("838a872e-a9cd-4c37-9418-47e30887ba5c"), "Software development" },
                    { new Guid("ad49be17-3efd-4bc5-9fd3-14a05349cb94"), "Accountant" },
                    { new Guid("b9812eda-7b0b-4cef-afee-adab395e274c"), "Finance" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("6b8dd2d4-24b3-47ae-ba70-6224b3b86c0c"));

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("838a872e-a9cd-4c37-9418-47e30887ba5c"));

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("ad49be17-3efd-4bc5-9fd3-14a05349cb94"));

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("b9812eda-7b0b-4cef-afee-adab395e274c"));

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("017cd214-ac14-45a5-9aff-b130d4d9f3d0"), "HR" },
                    { new Guid("597d9a32-e7ba-4ad5-ad03-0e7dd0db626d"), "Accountant" },
                    { new Guid("8b65c196-e84a-4a06-90f7-5525cc58f91f"), "Software development" },
                    { new Guid("bf3ab168-64b9-44b2-80d1-1b1055363899"), "Finance" }
                });
        }
    }
}
