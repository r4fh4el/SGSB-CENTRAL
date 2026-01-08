using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class indicecaracterizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_VEN_TE_CHOW",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_KIRPICH",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_DOOGE",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_CORPS_ENGINEERS",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_CARTER",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DB_DECLIVIDADE_BACIA",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DB_COMPRIMENTO_RIO_PRINCIPAL",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DB_AREA_DRENAGEM",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "INDICE_CARACTERIZACAO_BH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DB_AREA_BACIA_HIDROGRAFICA = table.Column<double>(type: "float", nullable: false),
                    DB_PERIMETRO = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO_RIO_PRINCIPAL = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO_VETORIAL = table.Column<double>(type: "float", nullable: false),
                    DB_ALTITUDE_VETORIAL = table.Column<double>(type: "float", nullable: false),
                    DB_ALTITUDE_MAXIMA_BACIA = table.Column<double>(type: "float", nullable: false),
                    DB_ALTITUDE_ALTIMETRICA_BACIA_M = table.Column<double>(type: "float", nullable: false),
                    DB_ALTITUDE_ALTIMETRICA_BACIA_KM = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO_AXIAL_BACIA = table.Column<double>(type: "float", nullable: false),
                    DB_RESULTADO_INDICE_CIRCULARIDADE = table.Column<double>(type: "float", nullable: false),
                    DB_FATOR_FORMA = table.Column<double>(type: "float", nullable: false),
                    DB_COEFICIENTE_COMPACIDADE = table.Column<double>(type: "float", nullable: false),
                    DB_DENSIDADE_DRENAGEM = table.Column<double>(type: "float", nullable: false),
                    DB_COEFICIENTE_MANUTENCAO = table.Column<double>(type: "float", nullable: false),
                    DB_GRADIENTE_CANAIS = table.Column<double>(type: "float", nullable: false),
                    DB_RELACAO_RELEVO = table.Column<double>(type: "float", nullable: false),
                    DB_INDICE_RUGOSIDADE = table.Column<double>(type: "float", nullable: false),
                    DB_SINUOSIDADE_CURSO_DAGUA = table.Column<double>(type: "float", nullable: false),
                    INT_BARRAGEM_ID = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDICE_CARACTERIZACAO_BH", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INDICE_CARACTERIZACAO_BH");

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_VEN_TE_CHOW",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_KIRPICH",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_DOOGE",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_CORPS_ENGINEERS",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DB_RESULTADO_CARTER",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DB_DECLIVIDADE_BACIA",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DB_COMPRIMENTO_RIO_PRINCIPAL",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DB_AREA_DRENAGEM",
                table: "TEMPO_CONCENTRACAO",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
