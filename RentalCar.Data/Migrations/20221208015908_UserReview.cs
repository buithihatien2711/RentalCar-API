using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCar.Data.Migrations
{
    public partial class UserReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //     migrationBuilder.CreateTable(
        //         name: "UserReviews",
        //         columns: table => new
        //         {
        //             Id = table.Column<int>(type: "int", nullable: false)
        //                 .Annotation("SqlServer:Identity", "1, 1"),
        //             Rating = table.Column<int>(type: "int", nullable: false),
        //             LeaseId = table.Column<int>(type: "int", nullable: false),
        //             RenterId = table.Column<int>(type: "int", nullable: false),
        //             Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //             CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
        //             UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_UserReviews", x => x.Id);
        //         });

        //     migrationBuilder.CreateTable(
        //         name: "UserReviewUsers",
        //         columns: table => new
        //         {
        //             UserId = table.Column<int>(type: "int", nullable: false),
        //             UserReviewId = table.Column<int>(type: "int", nullable: false),
        //             RoleId = table.Column<int>(type: "int", nullable: false)
        //         },
        //         constraints: table =>
        //         {
        //             table.PrimaryKey("PK_UserReviewUsers", x => new { x.UserId, x.UserReviewId });
        //             table.ForeignKey(
        //                 name: "FK_UserReviewUsers_UserReviews_UserReviewId",
        //                 column: x => x.UserReviewId,
        //                 principalTable: "UserReviews",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //             table.ForeignKey(
        //                 name: "FK_UserReviewUsers_Users_UserId",
        //                 column: x => x.UserId,
        //                 principalTable: "Users",
        //                 principalColumn: "Id",
        //                 onDelete: ReferentialAction.Cascade);
        //         });

        //     migrationBuilder.CreateIndex(
        //         name: "IX_UserReviewUsers_UserReviewId",
        //         table: "UserReviewUsers",
        //         column: "UserReviewId");
        // }

        // protected override void Down(MigrationBuilder migrationBuilder)
        // {
        //     migrationBuilder.DropTable(
        //         name: "UserReviewUsers");

        //     migrationBuilder.DropTable(
        //         name: "UserReviews");
        }
    }
}
