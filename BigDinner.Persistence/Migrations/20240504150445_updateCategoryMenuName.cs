using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigDinner.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateCategoryMenuName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_MenuCategory_MenuCategoryId",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuCategory",
                table: "MenuCategory");

            migrationBuilder.RenameTable(
                name: "MenuCategory",
                newName: "MenuCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId1",
                table: "MenuItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuCategories",
                table: "MenuCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId1",
                table: "MenuItems",
                column: "MenuId1",
                unique: true,
                filter: "[MenuId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuId1",
                table: "MenuItems",
                column: "MenuId1",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_MenuCategories_MenuCategoryId",
                table: "Menus",
                column: "MenuCategoryId",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuId1",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_MenuCategories_MenuCategoryId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuId1",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuCategories",
                table: "MenuCategories");

            migrationBuilder.DropColumn(
                name: "MenuId1",
                table: "MenuItems");

            migrationBuilder.RenameTable(
                name: "MenuCategories",
                newName: "MenuCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuCategory",
                table: "MenuCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_MenuCategory_MenuCategoryId",
                table: "Menus",
                column: "MenuCategoryId",
                principalTable: "MenuCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
