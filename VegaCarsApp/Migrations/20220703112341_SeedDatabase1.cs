using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VegaCarsApp.Migrations
{
    public partial class SeedDatabase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "id", "Name" },
                values: new object[] { 1, "Make1" });

            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "id", "Name" },
                values: new object[] { 2, "Make2" });

            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "id", "Name" },
                values: new object[] { 3, "Make3" });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "id", "MakeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Make1-ModelA" },
                    { 2, 1, "Make1-ModelB" },
                    { 3, 1, "Make1-ModelC" },
                    { 4, 2, "Make2-ModelA" },
                    { 5, 2, "Make2-ModelB" },
                    { 6, 2, "Make2-ModelC" },
                    { 7, 3, "Make3-ModelA" },
                    { 8, 3, "Make3-ModelB" },
                    { 9, 3, "Make3-ModelC" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
