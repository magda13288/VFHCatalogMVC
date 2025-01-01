using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class DataSeedForUserRegistrationForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "PlantMessages");

            migrationBuilder.DropColumn(
                name: "Inactivated",
                table: "PlantMessages");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "PlantMessages");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PlantMessages");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PlantMessages");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "PlantGrowthTypes");

            migrationBuilder.DropColumn(
                name: "Inactivated",
                table: "PlantGrowthTypes");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "PlantGrowthTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PlantGrowthTypes");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PlantGrowthTypes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "PlantGrowingSeazons");

            migrationBuilder.DropColumn(
                name: "Inactivated",
                table: "PlantGrowingSeazons");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "PlantGrowingSeazons");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PlantGrowingSeazons");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PlantGrowingSeazons");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "PlantDestinations");

            migrationBuilder.DropColumn(
                name: "Inactivated",
                table: "PlantDestinations");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "PlantDestinations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PlantDestinations");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PlantDestinations");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "NewUserPlants");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Poland" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Dolnośląskie" },
                    { 2, 1, "Kujawsko-Pomorskie" },
                    { 3, 1, "Lubelskie" },
                    { 4, 1, "Lubuskie" },
                    { 5, 1, "Łódzkie" },
                    { 6, 1, "Małopolskie" },
                    { 7, 1, "Mazowieckie" },
                    { 8, 1, "Opolskie" },
                    { 9, 1, "Podkarpackie" },
                    { 10, 1, "Podlaskie" },
                    { 11, 1, "Pomorskie" },
                    { 12, 1, "Śląskie" },
                    { 13, 1, "Świętokrzyskie" },
                    { 14, 1, "Warmińsko-Mazurskie" },
                    { 15, 1, "Wielkopolskie" },
                    { 16, 1, "Zachodniopomorskie" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "RegionId" },
                values: new object[,]
                {
                    { 1, "Katowice", 12 },
                    { 2, "Gliwice", 12 },
                    { 3, "Zabrze", 12 },
                    { 4, "Sosnowiec", 12 },
                    { 5, "Bytom", 12 },
                    { 6, "Rybnik", 12 },
                    { 7, "Chorzów", 12 },
                    { 8, "Tychy", 12 },
                    { 9, "Dąbrowa Górnicza", 12 },
                    { 10, "Jaworzno", 12 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PlantMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Inactivated",
                table: "PlantMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "PlantMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PlantMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PlantMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PlantGrowthTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Inactivated",
                table: "PlantGrowthTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "PlantGrowthTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PlantGrowthTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PlantGrowthTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PlantGrowingSeazons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Inactivated",
                table: "PlantGrowingSeazons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "PlantGrowingSeazons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PlantGrowingSeazons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PlantGrowingSeazons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PlantDestinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Inactivated",
                table: "PlantDestinations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "PlantDestinations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PlantDestinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PlantDestinations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "NewUserPlants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
