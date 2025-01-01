using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class AddingIdPropertyToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                column: "ConcurrencyStamp",
                value: "d72ad762-d626-4e4d-a2db-d4ee41e4abe7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Company",
                column: "ConcurrencyStamp",
                value: "77f520fd-fbad-4b96-b87d-8d43a180e473");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PrivateUser",
                column: "ConcurrencyStamp",
                value: "d72cc627-699f-46df-99d5-4f27a3a4fcf5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                column: "ConcurrencyStamp",
                value: "9a7c0139-f21c-497c-adb7-4bc4a2564bb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Company",
                column: "ConcurrencyStamp",
                value: "5cac8242-a1fe-40c9-b2f3-0f7e0285246e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PrivateUser",
                column: "ConcurrencyStamp",
                value: "e571f857-89d2-4480-9f47-98a8933ad78f");
        }
    }
}
