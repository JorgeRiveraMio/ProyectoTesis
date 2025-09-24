using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaToPreguntaFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 7,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "E", "En un grupo nuevo, prefiero: (A) Conocer y hablar con varias personas / (B) Observar primero y hablar solo con algunas.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 8,
                columns: new[] { "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "Cuando paso tiempo solo: (A) Me aburro fácilmente / (B) Me siento recargado.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 9,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "E", "En reuniones, suelo: (A) Hablar mucho y de manera espontánea / (B) Hablar solo cuando tengo algo importante.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 10,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "I", "Prefiero actividades: (A) Con mucha interacción social / (B) Tranquilas y personales.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 11,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "E", "Mis amigos me describen como: (A) Energético y sociable / (B) Reservado y reflexivo.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 12,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "I", "Cuando aprendo algo nuevo: (A) Me gusta discutirlo con otros / (B) Prefiero analizarlo solo." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 13,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "E", "Para relajarme: (A) Salgo con amigos / (B) Me quedo en casa leyendo o viendo algo." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 14,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "I", "En un viaje disfruto más: (A) Conocer mucha gente nueva / (B) Explorar a mi ritmo y en calma." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 15,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "E", "Al resolver un problema: (A) Pienso en voz alta con otros / (B) Reflexiono internamente." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 16,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "I", "Cuando me hacen una pregunta: (A) Contesto de inmediato / (B) Pienso antes de responder." });

            migrationBuilder.InsertData(
                table: "TBT_PREGUNTAS",
                columns: new[] { "IDD_PREGUNTA", "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[,]
                {
                    { 17, "S", "Prefiero fijarme en: (A) Los hechos concretos / (B) Las posibilidades futuras.", (byte)2 },
                    { 18, "N", "Cuando aprendo algo nuevo: (A) Necesito ejemplos prácticos / (B) Prefiero ideas y teorías generales.", (byte)2 },
                    { 19, "S", "Mi forma de trabajar es más: (A) Detallada y paso a paso / (B) Creativa y con saltos de ideas.", (byte)2 },
                    { 20, "N", "Al recordar algo tiendo a: (A) Enfocarme en los detalles / (B) Enfocarme en la idea principal.", (byte)2 },
                    { 21, "S", "En una conversación me gusta más: (A) Hablar de lo que está ocurriendo ahora / (B) Imaginar lo que podría ocurrir después.", (byte)2 },
                    { 22, "N", "Cuando sigo instrucciones: (A) Prefiero que sean claras y específicas / (B) Prefiero margen para interpretarlas.", (byte)2 },
                    { 23, "S", "Mis amigos dicen que soy: (A) Práctico y realista / (B) Soñador e imaginativo.", (byte)2 },
                    { 24, "N", "En un proyecto: (A) Me concentro en los pasos inmediatos / (B) Me concentro en el resultado final.", (byte)2 },
                    { 25, "S", "En clase me interesa más: (A) La aplicación práctica del tema / (B) La teoría y las conexiones.", (byte)2 },
                    { 26, "N", "En una historia: (A) Me fijo en los hechos / (B) Me fijo en el significado.", (byte)2 },
                    { 27, "T", "Al decidir: (A) Me baso en la lógica / (B) Me baso en lo que siento correcto.", (byte)2 },
                    { 28, "F", "Si un amigo comete un error: (A) Le digo directamente cómo mejorarlo / (B) Cuido sus sentimientos al corregirlo.", (byte)2 },
                    { 29, "J", "Prefiero: (A) Tener un plan definido / (B) Mantenerme flexible.", (byte)2 },
                    { 30, "P", "Antes de un viaje: (A) Organizo horarios y actividades / (B) Veo qué surge sobre la marcha.", (byte)2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 7,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "R", "Me gustaría trabajar en el servicio técnico de una empresa.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 8,
                columns: new[] { "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "Me gustaría trabajar en un centro de investigación o en un laboratorio.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 9,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "A", "En el futuro me gustaría escribir poemas, guiones de películas o de juegos de video.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 10,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "S", "Me gusta participar en organizaciones como la Cruz Roja o jóvenes exploradores.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 11,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "C", "Me gusta organizar mi trabajo día a día y para la semana.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 12,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { null, "En un grupo nuevo, prefiero: (A) Conocer y hablar con varias personas / (B) Observar primero y hablar solo con algunas." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 13,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { null, "Cuando paso tiempo solo: (A) Me aburro fácilmente / (B) Me siento recargado." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 14,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { null, "En reuniones, suelo: (A) Hablar mucho y de manera espontánea / (B) Hablar solo cuando tengo algo importante." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 15,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { null, "Prefiero actividades: (A) Con mucha interacción social / (B) Tranquilas y personales." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 16,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { null, "Para mí es más importante: (A) Terminar lo que empiezo / (B) Explorar nuevas cosas aunque no termine todas." });
        }
    }
}
