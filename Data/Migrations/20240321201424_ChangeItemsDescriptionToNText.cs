using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogR.Data.Migrations
{
    public partial class ChangeItemsDescriptionToNText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomText3",
                table: "Items",
                type: "NTEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomText2",
                table: "Items",
                type: "NTEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomText1",
                table: "Items",
                type: "NTEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomText3",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NTEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomText2",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NTEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomText1",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NTEXT",
                oldNullable: true);
        }
    }
}
