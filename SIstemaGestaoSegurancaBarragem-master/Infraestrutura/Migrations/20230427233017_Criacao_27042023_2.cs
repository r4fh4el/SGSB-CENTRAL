using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Criacao_27042023_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CondicaoFundacaoId",
                table: "Barragem",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.CreateTable(
                name: "CONDICAO_FUNDACAO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_NOME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONDICAO_FUNDACAO", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_CondicaoFundacaoId",
                table: "Barragem",
                column: "CondicaoFundacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barragem_CONDICAO_FUNDACAO_CondicaoFundacaoId",
                table: "Barragem",
                column: "CondicaoFundacaoId",
                principalTable: "CONDICAO_FUNDACAO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barragem_CONDICAO_FUNDACAO_CondicaoFundacaoId",
                table: "Barragem");

            migrationBuilder.DropTable(
                name: "CONDICAO_FUNDACAO");

            migrationBuilder.DropIndex(
                name: "IX_Barragem_CondicaoFundacaoId",
                table: "Barragem");

            migrationBuilder.DropColumn(
                name: "CondicaoFundacaoId",
                table: "Barragem");
        }
    }
}
