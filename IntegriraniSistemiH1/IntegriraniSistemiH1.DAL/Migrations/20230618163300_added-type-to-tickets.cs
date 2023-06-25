using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegriraniSistemiH1.DAL.Migrations
{
    public partial class addedtypetotickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Ticket");
        }
    }
}
