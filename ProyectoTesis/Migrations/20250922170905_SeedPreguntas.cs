using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class SeedPreguntas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBT_PREGUNTAS_TBT_MODULOS_MODULOIDD_MODULO",
                table: "TBT_PREGUNTAS");

            migrationBuilder.DropIndex(
                name: "IX_TBT_PREGUNTAS_MODULOIDD_MODULO",
                table: "TBT_PREGUNTAS");

            migrationBuilder.DropColumn(
                name: "MODULOIDD_MODULO",
                table: "TBT_PREGUNTAS");

            migrationBuilder.InsertData(
                table: "TBT_MODULOS",
                columns: new[] { "IDD_MODULO", "COD_MODULO_TX", "NOM_MODULO_TX" },
                values: new object[,]
                {
                    { (byte)1, "RIASEC", "Test RIASEC" },
                    { (byte)2, "MBTI", "Test MBTI" }
                });

            migrationBuilder.InsertData(
                table: "TBT_PREGUNTAS",
                columns: new[] { "IDD_PREGUNTA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[,]
                {
                    { 1, "Me gusta realizar pequeñas reparaciones de equipos electrodomésticos.", (byte)1 },
                    { 2, "El trabajo científico me parece muy interesante.", (byte)1 },
                    { 3, "Sé tocar un instrumento musical o me gustaría aprender.", (byte)1 },
                    { 4, "Me gustaría cuidar personas con enfermedades mentales.", (byte)1 },
                    { 5, "Me siento bien y me las arreglo cuando tengo que organizar el trabajo de mis compañeros y compañeras, fijarles tareas y comprobar si han sido realizadas.", (byte)1 },
                    { 6, "Me gusta llevar mis cuadernos de manera ordenada y limpia.", (byte)1 },
                    { 7, "Me gustaría trabajar en el servicio técnico de una empresa.", (byte)1 },
                    { 8, "Me gustaría trabajar en un centro de investigación o en un laboratorio.", (byte)1 },
                    { 9, "En el futuro me gustaría escribir poemas, guiones de películas o de juegos de video.", (byte)1 },
                    { 10, "Me gusta mucho participar en organizaciones no gubernamentales como la Cruz Roja o una organización de jóvenes exploradores.", (byte)1 },
                    { 11, "Me gusta organizar mi trabajo día a día y para la semana.", (byte)1 },
                    { 12, "En un grupo nuevo, prefiero: (A) Conocer y hablar con varias personas / (B) Observar primero y hablar solo con algunas.", (byte)2 },
                    { 13, "Cuando paso tiempo solo: (A) Me aburro fácilmente / (B) Me siento recargado.", (byte)2 },
                    { 14, "En reuniones, suelo: (A) Hablar mucho y de manera espontánea / (B) Hablar solo cuando tengo algo importante.", (byte)2 },
                    { 15, "Prefiero actividades: (A) Con mucha interacción social / (B) Tranquilas y personales.", (byte)2 },
                    { 16, "Para mí es más importante: (A) Terminar lo que empiezo / (B) Explorar nuevas cosas aunque no termine todas.", (byte)2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBT_PREGUNTAS_IDD_MODULO",
                table: "TBT_PREGUNTAS",
                column: "IDD_MODULO");

            migrationBuilder.AddForeignKey(
                name: "FK_TBT_PREGUNTAS_TBT_MODULOS_IDD_MODULO",
                table: "TBT_PREGUNTAS",
                column: "IDD_MODULO",
                principalTable: "TBT_MODULOS",
                principalColumn: "IDD_MODULO",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBT_PREGUNTAS_TBT_MODULOS_IDD_MODULO",
                table: "TBT_PREGUNTAS");

            migrationBuilder.DropIndex(
                name: "IX_TBT_PREGUNTAS_IDD_MODULO",
                table: "TBT_PREGUNTAS");

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TBT_MODULOS",
                keyColumn: "IDD_MODULO",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "TBT_MODULOS",
                keyColumn: "IDD_MODULO",
                keyValue: (byte)2);

            migrationBuilder.AddColumn<byte>(
                name: "MODULOIDD_MODULO",
                table: "TBT_PREGUNTAS",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_TBT_PREGUNTAS_MODULOIDD_MODULO",
                table: "TBT_PREGUNTAS",
                column: "MODULOIDD_MODULO");

            migrationBuilder.AddForeignKey(
                name: "FK_TBT_PREGUNTAS_TBT_MODULOS_MODULOIDD_MODULO",
                table: "TBT_PREGUNTAS",
                column: "MODULOIDD_MODULO",
                principalTable: "TBT_MODULOS",
                principalColumn: "IDD_MODULO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
