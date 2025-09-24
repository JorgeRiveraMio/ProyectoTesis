using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaToPregunta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "COD_CATEGORIA",
                table: "TBT_PREGUNTAS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 1,
                column: "COD_CATEGORIA",
                value: "R");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 2,
                column: "COD_CATEGORIA",
                value: "I");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 3,
                column: "COD_CATEGORIA",
                value: "A");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 4,
                column: "COD_CATEGORIA",
                value: "S");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 5,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "E", "Me las arreglo bien cuando tengo que organizar el trabajo de mis compañeros, fijarles tareas y comprobar si han sido realizadas." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 6,
                column: "COD_CATEGORIA",
                value: "C");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 7,
                column: "COD_CATEGORIA",
                value: "R");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 8,
                column: "COD_CATEGORIA",
                value: "I");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 9,
                column: "COD_CATEGORIA",
                value: "A");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 10,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "S", "Me gusta participar en organizaciones como la Cruz Roja o jóvenes exploradores." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 11,
                column: "COD_CATEGORIA",
                value: "C");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 12,
                column: "COD_CATEGORIA",
                value: null);

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 13,
                column: "COD_CATEGORIA",
                value: null);

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 14,
                column: "COD_CATEGORIA",
                value: null);

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 15,
                column: "COD_CATEGORIA",
                value: null);

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 16,
                column: "COD_CATEGORIA",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "COD_CATEGORIA",
                table: "TBT_PREGUNTAS");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 5,
                column: "DES_PREGUNTA_TX",
                value: "Me siento bien y me las arreglo cuando tengo que organizar el trabajo de mis compañeros y compañeras, fijarles tareas y comprobar si han sido realizadas.");

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 10,
                column: "DES_PREGUNTA_TX",
                value: "Me gusta mucho participar en organizaciones no gubernamentales como la Cruz Roja o una organización de jóvenes exploradores.");
        }
    }
}
