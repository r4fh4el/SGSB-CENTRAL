using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class CategoriaRisco3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA_RISCO",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INT_CT_A = table.Column<int>(type: "int", nullable: false),
                    INT_CT_B = table.Column<int>(type: "int", nullable: false),
                    INT_CT_C = table.Column<int>(type: "int", nullable: false),
                    INT_CT_D = table.Column<int>(type: "int", nullable: false),
                    INT_CT_E = table.Column<int>(type: "int", nullable: false),
                    INT_EC_H = table.Column<int>(type: "int", nullable: false),
                    INT_EC_I = table.Column<int>(type: "int", nullable: false),
                    INT_EC_J = table.Column<int>(type: "int", nullable: false),
                    INT_EC_L = table.Column<int>(type: "int", nullable: false),
                    INT_PS_N = table.Column<int>(type: "int", nullable: false),
                    INT_PS_O = table.Column<int>(type: "int", nullable: false),
                    INT_PS_P = table.Column<int>(type: "int", nullable: false),
                    INT_PS_Q = table.Column<int>(type: "int", nullable: false),
                    INT_PS_R = table.Column<int>(type: "int", nullable: false),
                    INT_VALOR_TOTAL = table.Column<int>(type: "int", nullable: false),
                    INT_VALOR_TOTAL_EC = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA_RISCO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CATEGORIA_RISCO_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_RISCO_BarragemId",
                table: "CATEGORIA_RISCO",
                column: "BarragemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CATEGORIA_RISCO");
        }
    }
}
