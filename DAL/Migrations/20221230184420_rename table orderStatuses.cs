using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class renametableorderStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblOrederStatuses",
                table: "tblOrederStatuses");

            migrationBuilder.RenameTable(
                name: "tblOrederStatuses",
                newName: "tblOrderStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblOrderStatuses",
                table: "tblOrderStatuses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblOrderStatuses",
                table: "tblOrderStatuses");

            migrationBuilder.RenameTable(
                name: "tblOrderStatuses",
                newName: "tblOrederStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblOrederStatuses",
                table: "tblOrederStatuses",
                column: "Id");
        }
    }
}
