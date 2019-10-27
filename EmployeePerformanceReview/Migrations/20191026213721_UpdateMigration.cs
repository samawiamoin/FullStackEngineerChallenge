using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeePerformanceReview.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFeedback",
                table: "PerformanceReviews",
                newName: "RequireFeedback");

            migrationBuilder.AlterColumn<int>(
                name: "AuthUserId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PRId",
                table: "Feedbacks",
                column: "PRId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AuthUserId",
                table: "Employees",
                column: "AuthUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AuthenticateUsers_AuthUserId",
                table: "Employees",
                column: "AuthUserId",
                principalTable: "AuthenticateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_PerformanceReviews_PRId",
                table: "Feedbacks",
                column: "PRId",
                principalTable: "PerformanceReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AuthenticateUsers_AuthUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_PerformanceReviews_PRId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_PRId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AuthUserId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "RequireFeedback",
                table: "PerformanceReviews",
                newName: "IsFeedback");

            migrationBuilder.AlterColumn<string>(
                name: "AuthUserId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
