using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogR.Data.Migrations
{
    public partial class AddTimeStampToItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Items");
        }
    }
}
