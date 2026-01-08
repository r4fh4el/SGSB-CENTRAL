using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class TipoEdificacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.CreateTable(
                name: "TIPO_EDIFICACAO",
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
                    table.PrimaryKey("PK_TIPO_EDIFICACAO", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARACTERIZACAO_AREA_JUSANTE_TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                column: "TipoEdificacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CARACTERIZACAO_AREA_JUSANTE_TIPO_EDIFICACAO_TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                column: "TipoEdificacaoId",
                principalTable: "TIPO_EDIFICACAO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARACTERIZACAO_AREA_JUSANTE_TIPO_EDIFICACAO_TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE");

            migrationBuilder.DropTable(
                name: "TIPO_EDIFICACAO");

            migrationBuilder.DropIndex(
                name: "IX_CARACTERIZACAO_AREA_JUSANTE_TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE");

            migrationBuilder.DropColumn(
                name: "TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE");
        }
    }
}
