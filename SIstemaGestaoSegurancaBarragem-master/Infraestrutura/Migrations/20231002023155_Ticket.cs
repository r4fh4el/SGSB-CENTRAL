using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TICKET",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_CODIDO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_TITULO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_DESCRICAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    INT_STATUS = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    INT_ID_USUARIO = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TICKET", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TICKET");
        }
    }
}
