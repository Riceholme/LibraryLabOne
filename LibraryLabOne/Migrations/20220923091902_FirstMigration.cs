using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryLabOne.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    inStock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Title", "inStock" },
                values: new object[,]
                {
                    { 1, "Harry Potter 1", false },
                    { 2, "Harry Potter 2", false },
                    { 3, "Harry Potter 3", true },
                    { 4, "Harry Potter 4", true },
                    { 5, "Harry Potter 5", true },
                    { 6, "Harry Potter 6", true },
                    { 7, "Harry Potter 7", true },
                    { 8, "Harry Potter 1", true },
                    { 9, "Harry Potter 2", false },
                    { 10, "Harry Potter 3", true },
                    { 11, "Harry Potter 4", true },
                    { 12, "Harry Potter 5", true },
                    { 13, "Harry Potter 6", true },
                    { 14, "Harry Potter 7", true },
                    { 15, "Harry Potter 1", true },
                    { 16, "Harry Potter 2", true },
                    { 17, "Harry Potter 3", true },
                    { 18, "Harry Potter 4", true },
                    { 19, "Harry Potter 5", true },
                    { 20, "Harry Potter 6", true },
                    { 21, "Harry Potter 7", true }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "knatte@ankbusiness.ank", "Knatte", "Anka" },
                    { 2, "fnatte@ankaab.ank", "Fnatte", "Anka" },
                    { 3, "tjatte@ankmail.ank", "Tjatte", "Anka" }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "BookId", "CustomerId", "EndDate", "ReturnedDate", "StartDate" },
                values: new object[] { 1, 1, 1, new DateTime(2022, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "BookId", "CustomerId", "EndDate", "ReturnedDate", "StartDate" },
                values: new object[] { 2, 2, 2, new DateTime(2022, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "BookId", "CustomerId", "EndDate", "ReturnedDate", "StartDate" },
                values: new object[] { 3, 9, 3, new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CustomerId",
                table: "Loans",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
