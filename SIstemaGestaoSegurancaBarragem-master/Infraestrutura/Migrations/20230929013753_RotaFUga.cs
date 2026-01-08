using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class RotaFUga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ROTA_FUGA",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_NOME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_LATITUDE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    STR_LONGITUDE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROTA_FUGA", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ROTA_FUGA");
        }
    }
}
