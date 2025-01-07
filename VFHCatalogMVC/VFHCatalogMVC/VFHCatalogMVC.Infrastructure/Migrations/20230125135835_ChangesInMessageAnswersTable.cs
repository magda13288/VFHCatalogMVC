using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class ChangesInMessageAnswersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAnswers_Messages_MessageId",
                table: "MessageAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageAnswers_MessageReceivers_MessageReceiverId",
                table: "MessageAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageAnswers",
                table: "MessageAnswers");

            migrationBuilder.DropIndex(
                name: "IX_MessageAnswers_MessageReceiverId",
                table: "MessageAnswers");

            migrationBuilder.DropColumn(
                name: "MessageReceiverId",
                table: "MessageAnswers");

            migrationBuilder.AddColumn<int>(
                name: "MessageAnswerId",
                table: "MessageAnswers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageAnswers",
                table: "MessageAnswers",
                columns: new[] { "MessageId", "MessageAnswerId" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAnswers_MessageAnswerId",
                table: "MessageAnswers",
                column: "MessageAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAnswers_Messages_MessageAnswerId",
                table: "MessageAnswers",
                column: "MessageAnswerId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAnswers_Messages_MessageId",
                table: "MessageAnswers",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAnswers_Messages_MessageAnswerId",
                table: "MessageAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageAnswers_Messages_MessageId",
                table: "MessageAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageAnswers",
                table: "MessageAnswers");

            migrationBuilder.DropIndex(
                name: "IX_MessageAnswers_MessageAnswerId",
                table: "MessageAnswers");

            migrationBuilder.DropColumn(
                name: "MessageAnswerId",
                table: "MessageAnswers");

            migrationBuilder.AddColumn<int>(
                name: "MessageReceiverId",
                table: "MessageAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageAnswers",
                table: "MessageAnswers",
                columns: new[] { "MessageId", "MessageReceiverId" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAnswers_MessageReceiverId",
                table: "MessageAnswers",
                column: "MessageReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAnswers_Messages_MessageId",
                table: "MessageAnswers",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAnswers_MessageReceivers_MessageReceiverId",
                table: "MessageAnswers",
                column: "MessageReceiverId",
                principalTable: "MessageReceivers",
                principalColumn: "Id");
        }
    }
}
