using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace December.DataAccess.Migrations
{
    public partial class UpApTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Adresses");
        }
    }
}
