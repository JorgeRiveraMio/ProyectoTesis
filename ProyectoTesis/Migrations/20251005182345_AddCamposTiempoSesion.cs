using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposTiempoSesion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FEC_FIN",
                table: "TBM_SESIONES",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FEC_INICIO",
                table: "TBM_SESIONES",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FEC_FIN",
                table: "TBM_SESIONES");

            migrationBuilder.DropColumn(
                name: "FEC_INICIO",
                table: "TBM_SESIONES");
        }
    }
}
