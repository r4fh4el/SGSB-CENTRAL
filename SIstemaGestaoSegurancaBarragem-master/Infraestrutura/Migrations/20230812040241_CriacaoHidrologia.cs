using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoHidrologia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DB_ALTITUDE_MINIMA_BACIA",
                table: "INDICE_CARACTERIZACAO_BH",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DB_COMPRIMENTO_TOTAL_RIO",
                table: "INDICE_CARACTERIZACAO_BH",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DB_ALTITUDE_MINIMA_BACIA",
                table: "INDICE_CARACTERIZACAO_BH");

            migrationBuilder.DropColumn(
                name: "DB_COMPRIMENTO_TOTAL_RIO",
                table: "INDICE_CARACTERIZACAO_BH");
        }
    }
}
