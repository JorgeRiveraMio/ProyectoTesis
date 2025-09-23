using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class FixIntentoModuloFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBM_INTENTOS_TBT_MODULOS_MODULOIDD_MODULO",
                table: "TBM_INTENTOS");

            migrationBuilder.DropIndex(
                name: "IX_TBM_INTENTOS_MODULOIDD_MODULO",
                table: "TBM_INTENTOS");

            migrationBuilder.DropColumn(
                name: "MODULOIDD_MODULO",
                table: "TBM_INTENTOS");

            migrationBuilder.CreateIndex(
                name: "IX_TBM_INTENTOS_IDD_MODULO",
                table: "TBM_INTENTOS",
                column: "IDD_MODULO");

            migrationBuilder.AddForeignKey(
                name: "FK_TBM_INTENTOS_TBT_MODULOS_IDD_MODULO",
                table: "TBM_INTENTOS",
                column: "IDD_MODULO",
                principalTable: "TBT_MODULOS",
                principalColumn: "IDD_MODULO",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBM_INTENTOS_TBT_MODULOS_IDD_MODULO",
                table: "TBM_INTENTOS");

            migrationBuilder.DropIndex(
                name: "IX_TBM_INTENTOS_IDD_MODULO",
                table: "TBM_INTENTOS");

            migrationBuilder.AddColumn<byte>(
                name: "MODULOIDD_MODULO",
                table: "TBM_INTENTOS",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_TBM_INTENTOS_MODULOIDD_MODULO",
                table: "TBM_INTENTOS",
                column: "MODULOIDD_MODULO");

            migrationBuilder.AddForeignKey(
                name: "FK_TBM_INTENTOS_TBT_MODULOS_MODULOIDD_MODULO",
                table: "TBM_INTENTOS",
                column: "MODULOIDD_MODULO",
                principalTable: "TBT_MODULOS",
                principalColumn: "IDD_MODULO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
