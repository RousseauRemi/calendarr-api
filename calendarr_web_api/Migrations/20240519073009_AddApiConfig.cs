using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace calendarr_web_api.Migrations
{
    /// <inheritdoc />
    public partial class AddApiConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiConfigEntities",
                columns: table => new
                {
                    Url = table.Column<string>(type: "text", nullable: false),
                    ConfigFromApiEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ConfigAndDataFromApiEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiConfigEntities", x => x.Url);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiConfigEntities");
        }
    }
}
