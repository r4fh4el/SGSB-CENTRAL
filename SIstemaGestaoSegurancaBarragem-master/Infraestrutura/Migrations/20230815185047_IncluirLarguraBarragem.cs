using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class IncluirLarguraBarragem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VAZAO_PICO",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DB_VHID = table.Column<double>(type: "float", nullable: true),
                    DB_HHID = table.Column<double>(type: "float", nullable: true),
                    DB_YMED = table.Column<double>(type: "float", nullable: true),
                    DB_AS = table.Column<double>(type: "float", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAZAO_PICO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VAZAO_PICO_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VAZAO_PICO_BarragemId",
                table: "VAZAO_PICO",
                column: "BarragemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VAZAO_PICO");
        }
    }
}
