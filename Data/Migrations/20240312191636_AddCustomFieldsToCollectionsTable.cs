using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogR.Data.Migrations
{
    public partial class AddCustomFieldsToCollectionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomBool1_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomBool1_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomBool2_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomBool2_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomBool3_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomBool3_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomDate1_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomDate1_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomDate2_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomDate2_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomDate3_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomDate3_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomInt1_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomInt1_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomInt2_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomInt2_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomInt3_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomInt3_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomString1_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomString1_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomString2_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomString2_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomString3_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomString3_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomText1_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomText1_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomText2_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomText2_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomText3_Name",
                table: "Collections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CustomText3_State",
                table: "Collections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomBool1_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomBool1_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomBool2_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomBool2_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomBool3_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomBool3_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomDate1_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomDate1_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomDate2_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomDate2_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomDate3_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomDate3_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomInt1_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomInt1_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomInt2_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomInt2_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomInt3_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomInt3_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomString1_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomString1_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomString2_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomString2_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomString3_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomString3_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomText1_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomText1_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomText2_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomText2_State",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomText3_Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CustomText3_State",
                table: "Collections");
        }
    }
}
