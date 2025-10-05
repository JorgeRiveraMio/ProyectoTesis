using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class AddTablaSatisfaccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBM_SATISFACCIONES",
                columns: table => new
                {
                    IDD_SATISFACCION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDD_SESION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FACILIDAD_USO = table.Column<int>(type: "int", nullable: false),
                    CLARIDAD_RESULTADOS = table.Column<int>(type: "int", nullable: false),
                    UTILIDAD_RECOMENDACIONES = table.Column<int>(type: "int", nullable: false),
                    SATISFACCION_GLOBAL = table.Column<int>(type: "int", nullable: false),
                    FEC_REGISTRO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBM_SATISFACCIONES", x => x.IDD_SATISFACCION);
                    table.ForeignKey(
                        name: "FK_TBM_SATISFACCIONES_TBM_SESIONES_IDD_SESION",
                        column: x => x.IDD_SESION,
                        principalTable: "TBM_SESIONES",
                        principalColumn: "IDD_SESION",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBM_SATISFACCIONES_IDD_SESION",
                table: "TBM_SATISFACCIONES",
                column: "IDD_SESION",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBM_SATISFACCIONES");
        }
    }
}
