using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GasturaApp.Migrations
{
    /// <inheritdoc />
    public partial class addColumnCorEmCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "Categorias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Categorias");
        }
    }
}
