using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCar.Data.Migrations
{
    public partial class AddRatingToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Users",
                newName: "RatingRent");

            migrationBuilder.AddColumn<double>(
                name: "RatingLease",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingLease",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RatingRent",
                table: "Users",
                newName: "Rating");
        }
    }
}
