using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GasturaApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarioInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "DataCadastro", "Email", "Nome", "Senha" },
                values: new object[] { 2, new DateTime(2025, 5, 12, 22, 52, 1, 634, DateTimeKind.Unspecified), "user@example.com", "Aninha", "12345665" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
