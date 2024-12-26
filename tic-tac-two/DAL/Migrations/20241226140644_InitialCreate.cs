using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    BoardSizeWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    BoardSizeHeight = table.Column<int>(type: "INTEGER", nullable: false),
                    GridSizeWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    GridSizeHeight = table.Column<int>(type: "INTEGER", nullable: false),
                    WinCondition = table.Column<int>(type: "INTEGER", nullable: false),
                    MovePieceAfterNMoves = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbSaveGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtDateTime = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 10240, nullable: false),
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbSaveGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbSaveGame_DbConfiguration_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "DbConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbSaveGame_ConfigurationId",
                table: "DbSaveGame",
                column: "ConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbSaveGame");

            migrationBuilder.DropTable(
                name: "DbConfiguration");
        }
    }
}
