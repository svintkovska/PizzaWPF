using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DAL.Migrations
{
    public partial class removeOrderStatustable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tgblOrders_tblOrderStatuses_StatusId",
                table: "tgblOrders");

            migrationBuilder.DropTable(
                name: "tblOrderStatuses");

            migrationBuilder.DropIndex(
                name: "IX_tgblOrders_StatusId",
                table: "tgblOrders");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "tgblOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "tgblOrders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tblOrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tgblOrders_StatusId",
                table: "tgblOrders",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_tgblOrders_tblOrderStatuses_StatusId",
                table: "tgblOrders",
                column: "StatusId",
                principalTable: "tblOrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
