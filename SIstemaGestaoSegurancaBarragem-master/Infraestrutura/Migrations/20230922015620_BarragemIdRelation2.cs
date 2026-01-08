using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class BarragemIdRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "VERTEDOURO",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "USO_BARRAGEM",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "TOMADA_DAGUA",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "TALUDE",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "SISTEMA_DRENAGEM",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "RESERVATORIO",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "INSTRUMENTOS",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "DESCARREGADOR_FUNDO",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "DADOS_GERAl",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "COTA_AREA_VOLUME",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "BarragemId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "VERTEDOURO");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "USO_BARRAGEM");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "TOMADA_DAGUA");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "TALUDE");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "SISTEMA_DRENAGEM");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "RESERVATORIO");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "INSTRUMENTOS");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "DESCARREGADOR_FUNDO");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "DADOS_GERAl");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "COTA_AREA_VOLUME");

            migrationBuilder.DropColumn(
                name: "BarragemId",
                table: "CARACTERIZACAO_AREA_JUSANTE");
        }
    }
}
