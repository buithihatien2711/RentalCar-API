using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCar.Data.Migrations
{
    public partial class EditCarReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "UpdateAt",
            //     table: "CarReviews");

            // migrationBuilder.AddColumn<DateTime>(
            //     name: "UpdatedAt",
            //     table: "CarReviews",
            //     type: "datetime2",
            //     nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CarReviews");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "CarReviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
