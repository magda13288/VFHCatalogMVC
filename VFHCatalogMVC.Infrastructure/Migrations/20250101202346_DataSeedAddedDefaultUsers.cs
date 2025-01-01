using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class DataSeedAddedDefaultUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                column: "ConcurrencyStamp",
                value: "57a2bced-4c68-431a-93ac-958d6936a7c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Company",
                column: "ConcurrencyStamp",
                value: "50e234a4-4ae7-4e25-964d-a8ac5d51ceaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PrivateUser",
                column: "ConcurrencyStamp",
                value: "027ca4a0-3682-471e-aba3-a4c1d52ab2af");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountName", "CEOLastName", "CEOName", "CompanyName", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "LogoPic", "NIP", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "REGON", "SecurityStamp", "TwoFactorEnabled", "UserName", "isActive" },
                values: new object[,]
                {
                    { "0a249d73-5e9a-4c07-9832-27645a2c2fe8", 0, "Admin", null, null, null, "88df6574-21db-41c6-b833-ce439584e236", "admin@gmail.com", true, null, null, true, null, null, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEDeZRsN0w0Gs6YSisBi9jbmg7ihLkvOxZgsuCjScMg2GD1JtcbU2tSzMjclvwSrSxA==", null, false, null, "Z4NQVBZ2LMDZAJM675CY3465JPFGY2PS", false, "admin@gmail.com", true },
                    { "2ef2b510-aa25-42ca-b68a-ee2fa0635924", 0, "Kinga", null, null, null, "cce6f078-25f9-4bb8-9af5-73cd0c91d645", "kinga123@gmail.com", true, null, null, true, null, null, null, "KINGA123@GMAIL.COM", "KINGA123@GMAIL.COM", "AQAAAAEAACcQAAAAECycEAX8sBrbNrgbYD2NdV0xoMgU2pJjfmsSi3J+ZczthajMzjaIuU5VMuKVyLGV/w==", null, false, null, "3QZ3HMO2U2QS27FOTYYOKNS2AYMMYTZM", false, "kinga123@gmail.com", true },
                    { "b9c413fb-7822-4bf2-8028-30597aab757b", 0, "Sara", null, null, null, "b5d8a96f-6606-472d-9646-61ff81fa5a1d", "sara2013@gmail.com", true, null, null, false, null, null, null, "SARA2013@GMAIL.COM", "SARA2013@GMAIL.COM", "AQAAAAEAACcQAAAAENew82n5Ros4D7PYuUpk0hyOVL6qkqteLtF1Wrz0uBd0BhoHnHd9VKfzUvIn/ySdRQ==", null, false, null, "QHE5B4ZDTU3QRAMHMMLIMHOOWXLK73S7", false, "sara2013@gmail.com", true }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "0a249d73-5e9a-4c07-9832-27645a2c2fe8", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2ef2b510-aa25-42ca-b68a-ee2fa0635924", "PrivateUser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "b9c413fb-7822-4bf2-8028-30597aab757b", "PrivateUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "0a249d73-5e9a-4c07-9832-27645a2c2fe8", "Admin" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2ef2b510-aa25-42ca-b68a-ee2fa0635924", "PrivateUser" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "b9c413fb-7822-4bf2-8028-30597aab757b", "PrivateUser" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0a249d73-5e9a-4c07-9832-27645a2c2fe8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ef2b510-aa25-42ca-b68a-ee2fa0635924");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c413fb-7822-4bf2-8028-30597aab757b");

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
    }
}
