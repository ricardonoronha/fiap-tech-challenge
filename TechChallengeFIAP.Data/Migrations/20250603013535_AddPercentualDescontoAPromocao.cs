using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallengeFIAP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPercentualDescontoAPromocao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PercentualDesconto",
                table: "Promocao",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentualDesconto",
                table: "Promocao");
        }
    }
}
