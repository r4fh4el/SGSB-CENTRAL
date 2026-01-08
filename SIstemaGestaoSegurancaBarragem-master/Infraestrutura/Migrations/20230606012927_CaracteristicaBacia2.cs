using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class CaracteristicaBacia2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CARACTERISTICA_BACIA",
                columns: table => new
                {
                    UsoSoloPredominanteId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TXT_NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TXT_CURSO_HIDRICO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    INT_CARACTERISTICA_PREDOMINANTE = table.Column<int>(type: "int", nullable: false),
                    DB_AREA_BACIA = table.Column<double>(type: "float", nullable: false),
                    DB_AREA_DRENAGEM = table.Column<double>(type: "float", nullable: false),
                    DB_PERIMETRO = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO_RIO_PRINCIPAL = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO_VETORIAL_RIO_PRINCIPAL = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO_TOTAL_RIO = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO_AXIAL = table.Column<double>(type: "float", nullable: false),
                    DB_ALTITUDE_MINIMA = table.Column<double>(type: "float", nullable: false),
                    DB_ALTITUDE_MAXIMA = table.Column<double>(type: "float", nullable: false),
                    DB_ALTITUDE_ALTIMETRICA_M = table.Column<double>(type: "float", nullable: false),
                    DB_ALTITUDE_ALTIMETRICA_KM = table.Column<double>(type: "float", nullable: false),
                    DB_DECLIVIDADE = table.Column<double>(type: "float", nullable: false),
                    TXT_MUNICIPIO_NASCENTE_RIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TXT_MUNICIPIO_FOZ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TXT_DEFINICAO_PADRAO_DRENAGEM = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARACTERISTICA_BACIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CARACTERISTICA_BACIA_USO_SOLO_PREDOMINANTE_UsoSoloPredominanteId",
                        column: x => x.UsoSoloPredominanteId,
                        principalTable: "USO_SOLO_PREDOMINANTE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARACTERISTICA_BACIA_UsoSoloPredominanteId",
                table: "CARACTERISTICA_BACIA",
                column: "UsoSoloPredominanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARACTERISTICA_BACIA");
        }
    }
}
