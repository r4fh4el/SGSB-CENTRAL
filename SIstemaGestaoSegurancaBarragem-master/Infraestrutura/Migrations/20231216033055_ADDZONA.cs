using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class ADDZONA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZONA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_ID_ZONA_NOME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STR_CPF = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STR_ENDERECO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STR_NOME_CIDADAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STR_NUMERO_TELEFONE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STR_NUMERO_PESSOAS = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    STR_OBSERVACAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STR_USUARIO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STR_RENDA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STR_AREA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    dataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZONA", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZONA");
        }
    }
}
