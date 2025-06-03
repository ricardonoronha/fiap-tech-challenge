using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallengeFIAP.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveValorPromocaoDoJogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorPromocao",
                table: "Jogo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorPromocao",
                table: "Jogo",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
