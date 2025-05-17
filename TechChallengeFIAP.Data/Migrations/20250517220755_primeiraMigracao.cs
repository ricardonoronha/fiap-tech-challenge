using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallengeFIAP.Data.Migrations
{
    /// <inheritdoc />
    public partial class primeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeJogo = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DescricaoJogo = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ClassificacaoJogo = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ValorBase = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    ValorPromocao = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    EhInativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    NomeUsuario = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    EmailUsuario = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    HashSenha = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    EhAdministrador = table.Column<bool>(type: "BIT", nullable: false),
                    EhAtivo = table.Column<bool>(type: "BIT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UsuarioCriador = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    UsuarioAtualizador = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promocao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataFim = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EhCancelada = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promocao_Jogo_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promocao_JogoId",
                table: "Promocao",
                column: "JogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Promocao");

            migrationBuilder.DropTable(
                name: "Jogo");
        }
    }
}
