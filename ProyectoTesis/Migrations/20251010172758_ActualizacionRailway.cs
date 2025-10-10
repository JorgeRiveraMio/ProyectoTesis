using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionRailway : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBM_ESTUDIANTES",
                columns: table => new
                {
                    IDD_ESTUDIANTE = table.Column<Guid>(type: "uuid", nullable: false),
                    NOM_COMPLETO = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    NUM_EDAD = table.Column<int>(type: "integer", nullable: false),
                    NOM_GENERO = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IDD_SESION = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBM_ESTUDIANTES", x => x.IDD_ESTUDIANTE);
                    table.ForeignKey(
                        name: "FK_TBM_ESTUDIANTES_TBM_SESIONES_IDD_SESION",
                        column: x => x.IDD_SESION,
                        principalTable: "TBM_SESIONES",
                        principalColumn: "IDD_SESION",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBM_ESTUDIANTES_IDD_SESION",
                table: "TBM_ESTUDIANTES",
                column: "IDD_SESION",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBM_ESTUDIANTES");
        }
    }
}
