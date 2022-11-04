using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class AddApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlantSeeds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlantSeedlings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlantOpinions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CustomerContactInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ContactDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CEOLastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CEOName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "LogoPic",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIP",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "REGON",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeeds_UserId",
                table: "PlantSeeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeedlings_UserId",
                table: "PlantSeedlings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantOpinions_UserId",
                table: "PlantOpinions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContactInformation_UserId",
                table: "CustomerContactInformation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_UserId",
                table: "ContactDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_AspNetUsers_UserId",
                table: "ContactDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContactInformation_AspNetUsers_UserId",
                table: "CustomerContactInformation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantOpinions_AspNetUsers_UserId",
                table: "PlantOpinions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantSeedlings_AspNetUsers_UserId",
                table: "PlantSeedlings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantSeeds_AspNetUsers_UserId",
                table: "PlantSeeds",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_AspNetUsers_UserId",
                table: "ContactDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContactInformation_AspNetUsers_UserId",
                table: "CustomerContactInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantOpinions_AspNetUsers_UserId",
                table: "PlantOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantSeedlings_AspNetUsers_UserId",
                table: "PlantSeedlings");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantSeeds_AspNetUsers_UserId",
                table: "PlantSeeds");

            migrationBuilder.DropIndex(
                name: "IX_PlantSeeds_UserId",
                table: "PlantSeeds");

            migrationBuilder.DropIndex(
                name: "IX_PlantSeedlings_UserId",
                table: "PlantSeedlings");

            migrationBuilder.DropIndex(
                name: "IX_PlantOpinions_UserId",
                table: "PlantOpinions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContactInformation_UserId",
                table: "CustomerContactInformation");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_UserId",
                table: "ContactDetails");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlantSeeds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlantSeedlings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlantOpinions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerContactInformation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CEOLastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CEOName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LogoPic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NIP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "REGON",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Addresses");
        }
    }
}
