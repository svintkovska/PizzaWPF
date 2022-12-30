using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DAL.Migrations
{
    public partial class addtblOrderStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desciption",
                table: "tblProducts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblProducts",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblOrederStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrederStatuses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOrederStatuses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblProducts");

            migrationBuilder.AddColumn<string>(
                name: "Desciption",
                table: "tblProducts",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true);
        }
    }
}
