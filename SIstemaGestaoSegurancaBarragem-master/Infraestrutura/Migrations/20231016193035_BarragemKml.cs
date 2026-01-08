using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class BarragemKml : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BARRAGEM_KML",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_COORDENADAS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BARRAGEM_KML", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BARRAGEM_KML");
        }
    }
}
