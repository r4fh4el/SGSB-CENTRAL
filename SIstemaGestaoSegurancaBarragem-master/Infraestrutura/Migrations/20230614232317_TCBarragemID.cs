using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class TCBarragemID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TEMPO_CONCENTRACAO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DB_BARRAGEM_ID = table.Column<int>(type: "int", nullable: false),
                    DB_COMPRIMENTO_RIO_PRINCIPAL = table.Column<double>(type: "float", nullable: true),
                    DB_DECLIVIDADE_BACIA = table.Column<double>(type: "float", nullable: true),
                    DB_AREA_DRENAGEM = table.Column<double>(type: "float", nullable: true),
                    DB_RESULTADO_KIRPICH = table.Column<double>(type: "float", nullable: true),
                    DB_RESULTADO_CORPS_ENGINEERS = table.Column<double>(type: "float", nullable: true),
                    DB_RESULTADO_CARTER = table.Column<double>(type: "float", nullable: true),
                    DB_RESULTADO_DOOGE = table.Column<double>(type: "float", nullable: true),
                    DB_RESULTADO_VEN_TE_CHOW = table.Column<double>(type: "float", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPO_CONCENTRACAO", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TEMPO_CONCENTRACAO");
        }
    }
}
