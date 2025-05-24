using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallengeFIAP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoPessoaEmRegistroEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId",
                table: "RegistroEvento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistroEvento_PessoaId",
                table: "RegistroEvento",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroEvento_Pessoa_PessoaId",
                table: "RegistroEvento",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroEvento_Pessoa_PessoaId",
                table: "RegistroEvento");

            migrationBuilder.DropIndex(
                name: "IX_RegistroEvento_PessoaId",
                table: "RegistroEvento");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "RegistroEvento");
        }
    }
}
