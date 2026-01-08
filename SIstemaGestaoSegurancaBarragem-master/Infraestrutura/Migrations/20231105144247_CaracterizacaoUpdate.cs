using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class CaracterizacaoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARACTERIZACAO_AREA_JUSANTE_TIPO_EDIFICACAO_TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE");

            migrationBuilder.AlterColumn<int>(
                name: "TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "INT_DISTANCIA",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_DATA_CADASTRO",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "datetime2",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_DATA_ALTERACAO",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "datetime2",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "BarragemId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CARACTERIZACAO_AREA_JUSANTE_TIPO_EDIFICACAO_TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                column: "TipoEdificacaoId",
                principalTable: "TIPO_EDIFICACAO",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARACTERIZACAO_AREA_JUSANTE_TIPO_EDIFICACAO_TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE");

            migrationBuilder.AlterColumn<int>(
                name: "TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "INT_DISTANCIA",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_DATA_CADASTRO",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "datetime2",
                maxLength: 50,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_DATA_ALTERACAO",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "datetime2",
                maxLength: 50,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BarragemId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CARACTERIZACAO_AREA_JUSANTE_TIPO_EDIFICACAO_TipoEdificacaoId",
                table: "CARACTERIZACAO_AREA_JUSANTE",
                column: "TipoEdificacaoId",
                principalTable: "TIPO_EDIFICACAO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
