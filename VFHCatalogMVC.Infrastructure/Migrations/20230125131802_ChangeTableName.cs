using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //    migrationBuilder.DropColumn(
            //        name: "isAnsweared",
            //        table: "MessageReceivers");

            //    migrationBuilder.DropColumn(
            //        name: "isRead",
            //        table: "MessageReceivers");

            //migrationBuilder.AddColumn<bool>(
            //    name: "isAnswer",
            //    table: "Messages",
            //    nullable: false,
            //    defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "isAnswer",
            //    table: "Messages");

            migrationBuilder.AddColumn<bool>(
                name: "isAnsweared",
                table: "MessageReceivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isRead",
                table: "MessageReceivers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
