using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class CaracteristicaBacia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barragem_CONDICAO_FUNDACAO_CondicaoFundacaoId",
                table: "Barragem");

            migrationBuilder.DropForeignKey(
                name: "FK_Barragem_TIPO_ESTRUTURA_BARRAGEM_TipoEstruturaBarragemId",
                table: "Barragem");

            migrationBuilder.DropForeignKey(
                name: "FK_Barragem_TIPO_MATERIAL_BARRAGEM_TipoMaterialBarragemId",
                table: "Barragem");

            migrationBuilder.DropIndex(
                name: "IX_Barragem_CondicaoFundacaoId",
                table: "Barragem");

            migrationBuilder.DropIndex(
                name: "IX_Barragem_TipoEstruturaBarragemId",
                table: "Barragem");

            migrationBuilder.DropIndex(
                name: "IX_Barragem_TipoMaterialBarragemId",
                table: "Barragem");

            migrationBuilder.DropColumn(
                name: "TipoEstruturaBarragemId",
                table: "Barragem");

            migrationBuilder.DropColumn(
                name: "TipoMaterialBarragemId",
                table: "Barragem");

            migrationBuilder.CreateTable(
                name: "DANO_POTENCIAL_ASSOCIADO",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INT_VTR_Q1 = table.Column<int>(type: "int", nullable: false),
                    INT_VTR_Q2 = table.Column<int>(type: "int", nullable: false),
                    INT_VTR_Q3 = table.Column<int>(type: "int", nullable: false),
                    INT_VTR_Q4 = table.Column<int>(type: "int", nullable: false),
                    INT_VTR_RESPOSTA = table.Column<int>(type: "int", nullable: false),
                    INT_PPV_Q1 = table.Column<int>(type: "int", nullable: false),
                    INT_PPV_Q2 = table.Column<int>(type: "int", nullable: false),
                    INT_PPV_Q3 = table.Column<int>(type: "int", nullable: false),
                    INT_PPV_Q4 = table.Column<int>(type: "int", nullable: false),
                    INT_PPV_RESPOSTA = table.Column<int>(type: "int", nullable: false),
                    INT_IA_Q1 = table.Column<int>(type: "int", nullable: false),
                    INT_IA_Q2 = table.Column<int>(type: "int", nullable: false),
                    INT_IA_RESPOSTA = table.Column<int>(type: "int", nullable: false),
                    INT_ISE_Q1 = table.Column<int>(type: "int", nullable: false),
                    INT_ISE_Q2 = table.Column<int>(type: "int", nullable: false),
                    INT_ISE_Q3 = table.Column<int>(type: "int", nullable: false),
                    INT_ISE_RESPOSTA = table.Column<int>(type: "int", nullable: false),
                    INT_DPA_TOTAL = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DANO_POTENCIAL_ASSOCIADO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DANO_POTENCIAL_ASSOCIADO_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DANO_POTENCIAL_ASSOCIADO_BarragemId",
                table: "DANO_POTENCIAL_ASSOCIADO",
                column: "BarragemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DANO_POTENCIAL_ASSOCIADO");

            migrationBuilder.AddColumn<int>(
                name: "TipoEstruturaBarragemId",
                table: "Barragem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoMaterialBarragemId",
                table: "Barragem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_CondicaoFundacaoId",
                table: "Barragem",
                column: "CondicaoFundacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_TipoEstruturaBarragemId",
                table: "Barragem",
                column: "TipoEstruturaBarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_TipoMaterialBarragemId",
                table: "Barragem",
                column: "TipoMaterialBarragemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barragem_CONDICAO_FUNDACAO_CondicaoFundacaoId",
                table: "Barragem",
                column: "CondicaoFundacaoId",
                principalTable: "CONDICAO_FUNDACAO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Barragem_TIPO_ESTRUTURA_BARRAGEM_TipoEstruturaBarragemId",
                table: "Barragem",
                column: "TipoEstruturaBarragemId",
                principalTable: "TIPO_ESTRUTURA_BARRAGEM",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Barragem_TIPO_MATERIAL_BARRAGEM_TipoMaterialBarragemId",
                table: "Barragem",
                column: "TipoMaterialBarragemId",
                principalTable: "TIPO_MATERIAL_BARRAGEM",
                principalColumn: "ID");
        }
    }
}
