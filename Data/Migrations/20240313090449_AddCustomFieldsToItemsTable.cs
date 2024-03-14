using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogR.Data.Migrations
{
    public partial class AddCustomFieldsToItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CustomBool1",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomBool2",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomBool3",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CustomDate1",
                table: "Items",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CustomDate2",
                table: "Items",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CustomDate3",
                table: "Items",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomInt1",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomInt2",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomInt3",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomString1",
                table: "Items",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomString2",
                table: "Items",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomString3",
                table: "Items",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomText1",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomText2",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomText3",
                table: "Items",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomBool1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomBool2",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomBool3",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomDate1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomDate2",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomDate3",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomInt1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomInt2",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomInt3",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomString1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomString2",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomString3",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomText1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomText2",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CustomText3",
                table: "Items");
        }
    }
}
