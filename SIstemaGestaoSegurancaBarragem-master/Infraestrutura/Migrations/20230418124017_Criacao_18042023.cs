using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Criacao_18042023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USR_IDADE = table.Column<int>(type: "int", nullable: false),
                    USR_CELULAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USR_TIPO = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CARACTERIZACAO_AREA_JUSANTE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INT_DISTANCIA = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARACTERIZACAO_AREA_JUSANTE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COTA_AREA_VOLUME",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_COTA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STR_VOLUME_ACUMULADO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STR_AREA_SUPERFICIE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COTA_AREA_VOLUME", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DADOS_GERAl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_NOME_EMPREENDEDOR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    INT_TIPO_EMPREENDEDOR = table.Column<int>(type: "int", nullable: false),
                    STR_ENDERECO_EMPREENDEDOR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_RESPONSAVEL_EMPREENDEDOR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_EMAIL_EMPREENDEDOR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_TELEFONE_EMPREENDEDOR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INT_QUANTIDADE_BARRAGEM = table.Column<int>(type: "int", nullable: false),
                    STR_LATITUDE_EMPREENDEDOR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    STR_LONGITUDE_EMPREENDEDOR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DADOS_GERAl", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DESCARREGADOR_FUNDO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_TIPO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_LOCALIZACAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DB_COTA_SOLEIRA_ENTRADA = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO = table.Column<double>(type: "float", nullable: false),
                    DB_DIMENSAO = table.Column<double>(type: "float", nullable: false),
                    STR_TIPO_COMPORTA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BOOL_TIPO_ACIONAMENTO_COMPORTA = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_FONTE_ALTERNATIVA_ENERGIA = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_COMANDO_DISTANCIA = table.Column<bool>(type: "bit", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DESCARREGADOR_FUNDO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "INSTRUMENTOS",
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
                    table.PrimaryKey("PK_INSTRUMENTOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RESERVATORIO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DB_CAPACIDADE_TOTAL_RESERVATORIO = table.Column<double>(type: "float", nullable: false),
                    DB_VOLUME_UTIL_RESERVATORIO = table.Column<double>(type: "float", nullable: false),
                    DB_BORDA_LIVRE = table.Column<double>(type: "float", nullable: false),
                    DB_MAXIMO_MAXIMORUM = table.Column<double>(type: "float", nullable: false),
                    DB_MAXIMO_NORMAL = table.Column<double>(type: "float", nullable: false),
                    DB_MINIMO_OPERACIONAL = table.Column<double>(type: "float", nullable: false),
                    DB_VOLUME_MORTO = table.Column<double>(type: "float", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVATORIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SISTEMA_DRENAGEM",
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
                    table.PrimaryKey("PK_SISTEMA_DRENAGEM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TALUDE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DB_INCLINACAO_TALUDE_MONTANTE = table.Column<double>(type: "float", nullable: false),
                    DB_INCLINACAO_TALUDE_JUSANTE = table.Column<double>(type: "float", nullable: false),
                    STR_PROTECAO_SUPERFICIE_MONTANTE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_PROTECAO_SUPERFICIE_JUSANTE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TALUDE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_ESTRUTURA_BARRAGEM",
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
                    table.PrimaryKey("PK_TIPO_ESTRUTURA_BARRAGEM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_MATERIAL_BARRAGEM",
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
                    table.PrimaryKey("PK_TIPO_MATERIAL_BARRAGEM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TOMADA_DAGUA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_LOCALIZACAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DB_COMPRIMENTO = table.Column<double>(type: "float", nullable: false),
                    STR_CONTROLE_ENTRADA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_CONTROLE_Saida = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DB_COTAS_TOMADAS_DAGUA_ENTRADA = table.Column<double>(type: "float", nullable: false),
                    BOOL_FONTE_ALTERNATIVA_ENERGIA = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_POSSIBILIDADE_MANOBRA_MANUAL = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_COMANDO_DISTANCIA = table.Column<bool>(type: "bit", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOMADA_DAGUA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USO_BARRAGEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_NOME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BOOL_GERACAO_ENERGIA = table.Column<bool>(type: "bit", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USO_BARRAGEM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VERTEDOURO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_TIPO_ESTRUTURA_EXTRAVASORA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BOOL_VERTEDOURO_CONTROLE = table.Column<bool>(type: "bit", nullable: false),
                    INT_QUANTIDADE_COMPORTA = table.Column<int>(type: "int", nullable: false),
                    BOOL_TIPO_ACIONAMENTO_COMPORTAS = table.Column<bool>(type: "bit", nullable: false),
                    STR_LOCALIZACAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DB_COMPRIMENTO_TOTAL_VERTEDOURO = table.Column<double>(type: "float", nullable: false),
                    DB_LARGURA_TOTAL_VERTEDOURO = table.Column<double>(type: "float", nullable: false),
                    DB_ALTURA = table.Column<double>(type: "float", nullable: false),
                    DB_COTA_SOLEIRO = table.Column<double>(type: "float", nullable: false),
                    BOOL_VETEDOURO_AUXILIAR = table.Column<bool>(type: "bit", nullable: false),
                    DB_TEMPO_RETORNO_VAZAO = table.Column<int>(type: "int", nullable: false),
                    DB_VAZAO = table.Column<double>(type: "float", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VERTEDOURO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Barragem",
                columns: table => new
                {
                    Tipo_Material_Id = table.Column<int>(type: "int", nullable: false),
                    Tipo_Estrutura_Id = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_BACIA_HIDROGRAFICA_ABRANGENCIA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_CURSO_DAGUA_BARRADO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_ANO_CONCLUSAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_IDADE_BARRAGEM = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_TIPO_FUNDACAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DB_ALTURA_MAXIMA = table.Column<double>(type: "float", nullable: false),
                    DB_COMPRIMENTO_COROAMENTO = table.Column<double>(type: "float", nullable: false),
                    DB_LARGURA_COROAMENTO_BARRAGEM = table.Column<double>(type: "float", nullable: false),
                    DB_COTA_COROAMENTO_BARRAGEM = table.Column<double>(type: "float", nullable: false),
                    TipoMaterialBarragemId = table.Column<int>(type: "int", nullable: true),
                    TipoEstruturaBarragemId = table.Column<int>(type: "int", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barragem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Barragem_TIPO_ESTRUTURA_BARRAGEM_TipoEstruturaBarragemId",
                        column: x => x.TipoEstruturaBarragemId,
                        principalTable: "TIPO_ESTRUTURA_BARRAGEM",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Barragem_TIPO_MATERIAL_BARRAGEM_TipoMaterialBarragemId",
                        column: x => x.TipoMaterialBarragemId,
                        principalTable: "TIPO_MATERIAL_BARRAGEM",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CARACTERIZACAO_AREA_JUSANTE_BARRAGEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaracterizacaoAreaJusanteBarragemId = table.Column<int>(type: "int", nullable: false),
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARACTERIZACAO_AREA_JUSANTE_BARRAGEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CARACTERIZACAO_AREA_JUSANTE_BARRAGEM_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CARACTERIZACAO_AREA_JUSANTE_BARRAGEM_CARACTERIZACAO_AREA_JUSANTE_CaracterizacaoAreaJusanteBarragemId",
                        column: x => x.CaracterizacaoAreaJusanteBarragemId,
                        principalTable: "CARACTERIZACAO_AREA_JUSANTE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DOCUMENTACAO_PROJETO_CONSTRUCAO_OPERACAO",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_PERGUNTA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BOOL_RESPOSTA = table.Column<bool>(type: "bit", nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENTACAO_PROJETO_CONSTRUCAO_OPERACAO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DOCUMENTACAO_PROJETO_CONSTRUCAO_OPERACAO_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INFORMACOES_COMPLEMENTARES",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_NOME_SETOR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BOOL_TEM_OUTORGA_CONSTRUCAO = table.Column<bool>(type: "bit", nullable: false),
                    STR_NUMERO_PORTARIA_OUTORGA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BOOL_TEM_LICENCA_OPERACAO = table.Column<bool>(type: "bit", nullable: false),
                    STR_NUMERO_PORTARIA_LICENCA_OPERACAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_VAZAO_MINIMA_RESTITUICAO_ANO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BOOL_TEM_VIGIA = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_TEM_OPERADOR24 = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_TEM_EQUIPE_OPERACAO = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_TEM_ESCRITORIO_LOCAL = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_TEM_EDIFICACAO_LOCAL = table.Column<bool>(type: "bit", nullable: false),
                    BOOL_TEM_HISTORICO_INCIDENTE_ACIDENTE = table.Column<bool>(type: "bit", nullable: false),
                    STR_HISTORICO_INCIDENTE_ACIDENTE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    INT_ANO_ULTIMA_REFORMA = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INFORMACOES_COMPLEMENTARES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INFORMACOES_COMPLEMENTARES_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INSPECOES",
                columns: table => new
                {
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STR_NOME_SETOR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    STR_FREQUENCIA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DT_DATA_ULTIMA_INSPECAO_ESPECIAL = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    DT_DATA_REVISAO_PERIODICA_RECENTE = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    BOOL_POSSUI_PAE = table.Column<bool>(type: "bit", nullable: false),
                    DT_DATA_PLANO_ACAO_EMERGENCIA = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    BOOL_POSSUI_ESTUDO_ROMPIMENTO = table.Column<bool>(type: "bit", nullable: false),
                    DT_DATA__ESTUDO_ROMPIMENTO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSPECOES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INSPECOES_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INSTRUMENTOS_BARRAGEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentosId = table.Column<int>(type: "int", nullable: false),
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTRUMENTOS_BARRAGEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INSTRUMENTOS_BARRAGEM_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INSTRUMENTOS_BARRAGEM_INSTRUMENTOS_InstrumentosId",
                        column: x => x.InstrumentosId,
                        principalTable: "INSTRUMENTOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SISTEMA_DRENAGEM_BARRAGEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SistemaDrenagemId = table.Column<int>(type: "int", nullable: false),
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SISTEMA_DRENAGEM_BARRAGEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SISTEMA_DRENAGEM_BARRAGEM_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SISTEMA_DRENAGEM_BARRAGEM_SISTEMA_DRENAGEM_SistemaDrenagemId",
                        column: x => x.SistemaDrenagemId,
                        principalTable: "SISTEMA_DRENAGEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USO_BARRAGEM_BARRAGEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsoBarragemId = table.Column<int>(type: "int", nullable: false),
                    BarragemId = table.Column<int>(type: "int", nullable: false),
                    DT_DATA_CADASTRO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DT_DATA_ALTERACAO = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USO_BARRAGEM_BARRAGEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USO_BARRAGEM_BARRAGEM_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USO_BARRAGEM_BARRAGEM_USO_BARRAGEM_UsoBarragemId",
                        column: x => x.UsoBarragemId,
                        principalTable: "USO_BARRAGEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_TipoEstruturaBarragemId",
                table: "Barragem",
                column: "TipoEstruturaBarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_TipoMaterialBarragemId",
                table: "Barragem",
                column: "TipoMaterialBarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_CARACTERIZACAO_AREA_JUSANTE_BARRAGEM_BarragemId",
                table: "CARACTERIZACAO_AREA_JUSANTE_BARRAGEM",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_CARACTERIZACAO_AREA_JUSANTE_BARRAGEM_CaracterizacaoAreaJusanteBarragemId",
                table: "CARACTERIZACAO_AREA_JUSANTE_BARRAGEM",
                column: "CaracterizacaoAreaJusanteBarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTACAO_PROJETO_CONSTRUCAO_OPERACAO_BarragemId",
                table: "DOCUMENTACAO_PROJETO_CONSTRUCAO_OPERACAO",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_INFORMACOES_COMPLEMENTARES_BarragemId",
                table: "INFORMACOES_COMPLEMENTARES",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_INSPECOES_BarragemId",
                table: "INSPECOES",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTRUMENTOS_BARRAGEM_BarragemId",
                table: "INSTRUMENTOS_BARRAGEM",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTRUMENTOS_BARRAGEM_InstrumentosId",
                table: "INSTRUMENTOS_BARRAGEM",
                column: "InstrumentosId");

            migrationBuilder.CreateIndex(
                name: "IX_SISTEMA_DRENAGEM_BARRAGEM_BarragemId",
                table: "SISTEMA_DRENAGEM_BARRAGEM",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_SISTEMA_DRENAGEM_BARRAGEM_SistemaDrenagemId",
                table: "SISTEMA_DRENAGEM_BARRAGEM",
                column: "SistemaDrenagemId");

            migrationBuilder.CreateIndex(
                name: "IX_USO_BARRAGEM_BARRAGEM_BarragemId",
                table: "USO_BARRAGEM_BARRAGEM",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_USO_BARRAGEM_BARRAGEM_UsoBarragemId",
                table: "USO_BARRAGEM_BARRAGEM",
                column: "UsoBarragemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CARACTERIZACAO_AREA_JUSANTE_BARRAGEM");

            migrationBuilder.DropTable(
                name: "COTA_AREA_VOLUME");

            migrationBuilder.DropTable(
                name: "DADOS_GERAl");

            migrationBuilder.DropTable(
                name: "DESCARREGADOR_FUNDO");

            migrationBuilder.DropTable(
                name: "DOCUMENTACAO_PROJETO_CONSTRUCAO_OPERACAO");

            migrationBuilder.DropTable(
                name: "INFORMACOES_COMPLEMENTARES");

            migrationBuilder.DropTable(
                name: "INSPECOES");

            migrationBuilder.DropTable(
                name: "INSTRUMENTOS_BARRAGEM");

            migrationBuilder.DropTable(
                name: "RESERVATORIO");

            migrationBuilder.DropTable(
                name: "SISTEMA_DRENAGEM_BARRAGEM");

            migrationBuilder.DropTable(
                name: "TALUDE");

            migrationBuilder.DropTable(
                name: "TOMADA_DAGUA");

            migrationBuilder.DropTable(
                name: "USO_BARRAGEM_BARRAGEM");

            migrationBuilder.DropTable(
                name: "VERTEDOURO");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CARACTERIZACAO_AREA_JUSANTE");

            migrationBuilder.DropTable(
                name: "INSTRUMENTOS");

            migrationBuilder.DropTable(
                name: "SISTEMA_DRENAGEM");

            migrationBuilder.DropTable(
                name: "Barragem");

            migrationBuilder.DropTable(
                name: "USO_BARRAGEM");

            migrationBuilder.DropTable(
                name: "TIPO_ESTRUTURA_BARRAGEM");

            migrationBuilder.DropTable(
                name: "TIPO_MATERIAL_BARRAGEM");
        }
    }
}
