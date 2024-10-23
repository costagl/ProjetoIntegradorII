using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoEmailSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidato",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Endereco = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidato", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    CNPJ = table.Column<string>(type: "varchar(18)", unicode: false, maxLength: 18, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.CNPJ);
                });

            migrationBuilder.CreateTable(
                name: "Experiencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF_Candidato = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    Empresa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: true),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Experiencia_Candidato",
                        column: x => x.CPF_Candidato,
                        principalTable: "Candidato",
                        principalColumn: "CPF");
                });

            migrationBuilder.CreateTable(
                name: "Formacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF_Candidato = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    Instituicao = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Curso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: true),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_Formacao_Candidato",
                        column: x => x.CPF_Candidato,
                        principalTable: "Candidato",
                        principalColumn: "CPF");
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    id = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    CPF_Candidato = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    CNPJ_Empresa = table.Column<string>(type: "varchar(18)", unicode: false, maxLength: 18, nullable: false),
                    Classificacao = table.Column<byte>(type: "tinyint", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Candidato",
                        column: x => x.CPF_Candidato,
                        principalTable: "Candidato",
                        principalColumn: "CPF");
                    table.ForeignKey(
                        name: "FK_Avaliacao_Empresa",
                        column: x => x.CNPJ_Empresa,
                        principalTable: "Empresa",
                        principalColumn: "CNPJ");
                });

            migrationBuilder.CreateTable(
                name: "Vaga",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TituloVaga = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CNPJ_Empresa = table.Column<string>(type: "varchar(18)", unicode: false, maxLength: 18, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false),
                    Requisitos = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true),
                    AreaAtuacao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Modelo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Localizacao = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    TipoContrato = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: true),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaga", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vaga_Empresa",
                        column: x => x.CNPJ_Empresa,
                        principalTable: "Empresa",
                        principalColumn: "CNPJ");
                });

            migrationBuilder.CreateTable(
                name: "Candidatura",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF_Candidato = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    idVaga = table.Column<int>(type: "int", nullable: false),
                    DataCandidatura = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CartaApresetancao = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true),
                    Curriculo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Candidatura_Candidato",
                        column: x => x.CPF_Candidato,
                        principalTable: "Candidato",
                        principalColumn: "CPF");
                    table.ForeignKey(
                        name: "FK_Candidatura_Vaga",
                        column: x => x.idVaga,
                        principalTable: "Vaga",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_CNPJ_Empresa",
                table: "Avaliacao",
                column: "CNPJ_Empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_CPF_Candidato",
                table: "Avaliacao",
                column: "CPF_Candidato");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatura_CPF_Candidato",
                table: "Candidatura",
                column: "CPF_Candidato");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatura_idVaga",
                table: "Candidatura",
                column: "idVaga");

            migrationBuilder.CreateIndex(
                name: "IX_Experiencia_CPF_Candidato",
                table: "Experiencia",
                column: "CPF_Candidato");

            migrationBuilder.CreateIndex(
                name: "IX_Formacao_CPF_Candidato",
                table: "Formacao",
                column: "CPF_Candidato");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_CNPJ_Empresa",
                table: "Vaga",
                column: "CNPJ_Empresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "Candidatura");

            migrationBuilder.DropTable(
                name: "Experiencia");

            migrationBuilder.DropTable(
                name: "Formacao");

            migrationBuilder.DropTable(
                name: "Vaga");

            migrationBuilder.DropTable(
                name: "Candidato");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
