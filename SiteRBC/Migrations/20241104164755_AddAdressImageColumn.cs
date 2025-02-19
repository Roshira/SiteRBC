using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteRBC.Migrations
{
    /// <inheritdoc />
    public partial class AddAdressImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdressImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdressImage",
                table: "Products");
        }
    }
}
