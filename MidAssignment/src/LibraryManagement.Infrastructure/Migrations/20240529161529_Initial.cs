using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoverPath = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    BookPath = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    BookCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => x.BookCategoryId);
                    table.ForeignKey(
                        name: "FK_BookCategories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowingRequests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Requestor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Approver = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowingRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequests_User_Requestor",
                        column: x => x.Requestor,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowingRequestDetails",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastReadPageNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowingRequestDetails", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequestDetails_BookBorrowingRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "BookBorrowingRequests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBorrowingRequestDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequestDetails_BookId",
                table: "BookBorrowingRequestDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequests_Requestor",
                table: "BookBorrowingRequests",
                column: "Requestor");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_BookId",
                table: "BookCategories",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_CategoryId",
                table: "BookCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn",
                table: "Books",
                column: "Isbn",
                unique: true,
                filter: "[Isbn] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBorrowingRequestDetails");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "BookBorrowingRequests");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
