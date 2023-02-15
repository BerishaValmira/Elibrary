using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMngSys.Migrations
{
    public partial class Add_Column_To_Book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Book",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Book");
        }
    }
}
