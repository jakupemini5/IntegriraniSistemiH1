using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegriraniSistemiH1.Data.Migrations
{
    public partial class addedprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Ticket",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ticket");
        }
    }
}
