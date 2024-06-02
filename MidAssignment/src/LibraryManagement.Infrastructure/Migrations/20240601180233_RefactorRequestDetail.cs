using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorRequestDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookBorrowingRequestDetails",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BookBorrowingRequestDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookBorrowingRequestDetails",
                table: "BookBorrowingRequestDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequestDetails_RequestId",
                table: "BookBorrowingRequestDetails",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookBorrowingRequestDetails",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookBorrowingRequestDetails_RequestId",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookBorrowingRequestDetails",
                table: "BookBorrowingRequestDetails",
                column: "RequestId");
        }
    }
}
