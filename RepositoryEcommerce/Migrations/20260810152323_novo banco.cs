using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class novobanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_ClienteId1",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ClienteId1",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_Status",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Pedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId1",
                table: "Pedidos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId1",
                table: "Pedidos",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Status",
                table: "Pedidos",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_ClienteId1",
                table: "Pedidos",
                column: "ClienteId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
