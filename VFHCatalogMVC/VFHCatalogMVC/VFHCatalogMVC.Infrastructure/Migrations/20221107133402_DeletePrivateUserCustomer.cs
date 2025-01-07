using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class DeletePrivateUserCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PrivateUsers_PrivateUserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Customers_CustomerID",
                table: "ContactDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContactInformation_Customers_CustomerId",
                table: "CustomerContactInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantOpinions_Customers_CustomerId",
                table: "PlantOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantOpinions_PrivateUsers_PrivateUserId",
                table: "PlantOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantSeedlings_Customers_CustomerId",
                table: "PlantSeedlings");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantSeedlings_PrivateUsers_PrivateUserId",
                table: "PlantSeedlings");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantSeeds_Customers_CustomerId",
                table: "PlantSeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantSeeds_PrivateUsers_PrivateUserId",
                table: "PlantSeeds");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PrivateUsers");

            migrationBuilder.DropIndex(
                name: "IX_PlantSeeds_CustomerId",
                table: "PlantSeeds");

            migrationBuilder.DropIndex(
                name: "IX_PlantSeeds_PrivateUserId",
                table: "PlantSeeds");

            migrationBuilder.DropIndex(
                name: "IX_PlantSeedlings_CustomerId",
                table: "PlantSeedlings");

            migrationBuilder.DropIndex(
                name: "IX_PlantSeedlings_PrivateUserId",
                table: "PlantSeedlings");

            migrationBuilder.DropIndex(
                name: "IX_PlantOpinions_CustomerId",
                table: "PlantOpinions");

            migrationBuilder.DropIndex(
                name: "IX_PlantOpinions_PrivateUserId",
                table: "PlantOpinions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContactInformation_CustomerId",
                table: "CustomerContactInformation");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_CustomerID",
                table: "ContactDetails");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PrivateUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "PlantSeeds");

            migrationBuilder.DropColumn(
                name: "PrivateUserId",
                table: "PlantSeeds");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "PlantSeedlings");

            migrationBuilder.DropColumn(
                name: "PrivateUserId",
                table: "PlantSeedlings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "PlantOpinions");

            migrationBuilder.DropColumn(
                name: "PrivateUserId",
                table: "PlantOpinions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerContactInformation");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PrivateUserId",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "PlantSeeds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateUserId",
                table: "PlantSeeds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "PlantSeedlings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateUserId",
                table: "PlantSeedlings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "PlantOpinions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateUserId",
                table: "PlantOpinions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "CustomerContactInformation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerID",
                table: "ContactDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateUserId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CEOLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEOName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoPic = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REGON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeeds_CustomerId",
                table: "PlantSeeds",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeeds_PrivateUserId",
                table: "PlantSeeds",
                column: "PrivateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeedlings_CustomerId",
                table: "PlantSeedlings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSeedlings_PrivateUserId",
                table: "PlantSeedlings",
                column: "PrivateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantOpinions_CustomerId",
                table: "PlantOpinions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantOpinions_PrivateUserId",
                table: "PlantOpinions",
                column: "PrivateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContactInformation_CustomerId",
                table: "CustomerContactInformation",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_CustomerID",
                table: "ContactDetails",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PrivateUserId",
                table: "Addresses",
                column: "PrivateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PrivateUsers_PrivateUserId",
                table: "Addresses",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Customers_CustomerID",
                table: "ContactDetails",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContactInformation_Customers_CustomerId",
                table: "CustomerContactInformation",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantOpinions_Customers_CustomerId",
                table: "PlantOpinions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantOpinions_PrivateUsers_PrivateUserId",
                table: "PlantOpinions",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantSeedlings_Customers_CustomerId",
                table: "PlantSeedlings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantSeedlings_PrivateUsers_PrivateUserId",
                table: "PlantSeedlings",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantSeeds_Customers_CustomerId",
                table: "PlantSeeds",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantSeeds_PrivateUsers_PrivateUserId",
                table: "PlantSeeds",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
