using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigDinner.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addMenuCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MenuCategoryId",
                table: "Menus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MenuCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuCategoryId",
                table: "Menus",
                column: "MenuCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_MenuCategory_MenuCategoryId",
                table: "Menus",
                column: "MenuCategoryId",
                principalTable: "MenuCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_MenuCategory_MenuCategoryId",
                table: "Menus");

            migrationBuilder.DropTable(
                name: "MenuCategory");

            migrationBuilder.DropIndex(
                name: "IX_Menus_MenuCategoryId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "MenuCategoryId",
                table: "Menus");
        }
    }
}
