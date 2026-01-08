using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Tipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TIPO_EDIFICACAO_BARRAGEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoEdificacaoId = table.Column<int>(type: "int", nullable: false),
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_EDIFICACAO_BARRAGEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TIPO_EDIFICACAO_BARRAGEM_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPO_EDIFICACAO_BARRAGEM_TIPO_EDIFICACAO_TipoEdificacaoId",
                        column: x => x.TipoEdificacaoId,
                        principalTable: "TIPO_EDIFICACAO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TIPO_EDIFICACAO_BARRAGEM_BarragemId",
                table: "TIPO_EDIFICACAO_BARRAGEM",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_TIPO_EDIFICACAO_BARRAGEM_TipoEdificacaoId",
                table: "TIPO_EDIFICACAO_BARRAGEM",
                column: "TipoEdificacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TIPO_EDIFICACAO_BARRAGEM");
        }
    }
}
