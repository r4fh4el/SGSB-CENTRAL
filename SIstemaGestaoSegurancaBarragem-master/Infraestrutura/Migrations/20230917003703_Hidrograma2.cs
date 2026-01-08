using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Hidrograma2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HIDROGRAMA_PARABOLICO",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DB_VALOR_VAZAO = table.Column<double>(type: "float", nullable: true),
                    DB_QP = table.Column<double>(type: "float", nullable: true),
                    DB_TEMPO_HORA = table.Column<double>(type: "float", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HIDROGRAMA_PARABOLICO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HIDROGRAMA_PARABOLICO_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HIDROGRAMA_TRIANGULAR",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DB_VOLUMES_RESERVATORIO = table.Column<double>(type: "float", nullable: true),
                    DB_QP = table.Column<double>(type: "float", nullable: true),
                    DB_TEMPO_PICO = table.Column<double>(type: "float", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HIDROGRAMA_TRIANGULAR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HIDROGRAMA_TRIANGULAR_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEMPO_RUPTURA",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DB_VALOR_TEMPO_RUPTURA = table.Column<double>(type: "float", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPO_RUPTURA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TEMPO_RUPTURA_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HIDROGRAMA_PARABOLICO_BarragemId",
                table: "HIDROGRAMA_PARABOLICO",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_HIDROGRAMA_TRIANGULAR_BarragemId",
                table: "HIDROGRAMA_TRIANGULAR",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_TEMPO_RUPTURA_BarragemId",
                table: "TEMPO_RUPTURA",
                column: "BarragemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HIDROGRAMA_PARABOLICO");

            migrationBuilder.DropTable(
                name: "HIDROGRAMA_TRIANGULAR");

            migrationBuilder.DropTable(
                name: "TEMPO_RUPTURA");
        }
    }
}
