using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogR.Data.Migrations
{
    public partial class AddFullTextIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Migrations", "20240319161531_AddFullTextIndexes.sql");
            string sql = File.ReadAllText(path);
            migrationBuilder.Sql(sql, true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Migrations", "20240319161531_AddFullTextIndexesUndo.sql");
            string sql = File.ReadAllText(path);
            migrationBuilder.Sql(sql, true);
        }
    }
}
