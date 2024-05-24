using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigDinner.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateMenuAggregateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Menus_MenuId",
                table: "MenuItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "MenuId",
                table: "MenuItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Menus_MenuId",
                table: "MenuItem",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Menus_MenuId",
                table: "MenuItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "MenuId",
                table: "MenuItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Menus_MenuId",
                table: "MenuItem",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
