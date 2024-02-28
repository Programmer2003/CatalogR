using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogR.Data.Migrations
{
    public partial class AddTopicIdToCollectionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionTopicId",
                table: "Collections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CollectionTopicId",
                table: "Collections",
                column: "CollectionTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_CollectionTopics_CollectionTopicId",
                table: "Collections",
                column: "CollectionTopicId",
                principalTable: "CollectionTopics",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_CollectionTopics_CollectionTopicId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CollectionTopicId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CollectionTopicId",
                table: "Collections");
        }
    }
}
