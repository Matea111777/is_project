using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class GuidAuthorID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Authors_AuthorId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_AuthorId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Publishers");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorsId",
                table: "Publishers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_AuthorsId",
                table: "Publishers",
                column: "AuthorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Authors_AuthorsId",
                table: "Publishers",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Authors_AuthorsId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_AuthorsId",
                table: "Publishers");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorsId",
                table: "Publishers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Publishers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_AuthorId",
                table: "Publishers",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Authors_AuthorId",
                table: "Publishers",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");
        }
    }
}
