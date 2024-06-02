using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePropertiesBorrowingRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequests_User_Requestor",
                table: "BookBorrowingRequests");

            migrationBuilder.RenameColumn(
                name: "Requestor",
                table: "BookBorrowingRequests",
                newName: "RequestorId");

            migrationBuilder.RenameColumn(
                name: "Approver",
                table: "BookBorrowingRequests",
                newName: "ApproverId");

            migrationBuilder.RenameIndex(
                name: "IX_BookBorrowingRequests_Requestor",
                table: "BookBorrowingRequests",
                newName: "IX_BookBorrowingRequests_RequestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequests_User_RequestorId",
                table: "BookBorrowingRequests",
                column: "RequestorId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequests_User_RequestorId",
                table: "BookBorrowingRequests");

            migrationBuilder.RenameColumn(
                name: "RequestorId",
                table: "BookBorrowingRequests",
                newName: "Requestor");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "BookBorrowingRequests",
                newName: "Approver");

            migrationBuilder.RenameIndex(
                name: "IX_BookBorrowingRequests_RequestorId",
                table: "BookBorrowingRequests",
                newName: "IX_BookBorrowingRequests_Requestor");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequests_User_Requestor",
                table: "BookBorrowingRequests",
                column: "Requestor",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
