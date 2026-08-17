using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoUsuairo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Usuarios",
                newName: "TipoUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoUsuario",
                table: "Usuarios",
                newName: "Discriminator");
        }
    }
}
