using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposRecomendacionesResultado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LISTA_RECOMENDACIONES_JSON",
                table: "TBM_RESULTADOS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NUM_RECOMENDACIONES",
                table: "TBM_RESULTADOS",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LISTA_RECOMENDACIONES_JSON",
                table: "TBM_RESULTADOS");

            migrationBuilder.DropColumn(
                name: "NUM_RECOMENDACIONES",
                table: "TBM_RESULTADOS");
        }
    }
}
