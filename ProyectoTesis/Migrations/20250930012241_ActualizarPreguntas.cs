using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarPreguntas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TBT_MODULOS",
                keyColumn: "IDD_MODULO",
                keyValue: (byte)2,
                columns: new[] { "COD_MODULO_TX", "NOM_MODULO_TX" },
                values: new object[] { "OCEAN", "Test Big Five (OCEAN)" });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 2,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "R", "Me gustaría trabajar en el servicio técnico de una empresa." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 3,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "R", "Me interesa conocer el diseño y funcionamiento de los equipos técnicos." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 4,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "I", "El trabajo científico me parece muy interesante." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 5,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "I", "Me interesan los descubrimientos científicos y las nuevas invenciones." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 6,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "I", "Me gustaría realizar estudios y descubrir la vacuna contra una enfermedad grave." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 7,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "A", "Sé tocar un instrumento musical o me gustaría aprender.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 8,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "A", "Me gusta ver exposiciones de esculturas, pintura o fotografía.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 9,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "A", "Me gustaría expresarme mediante una actividad creativa como la pintura, el dibujo o la música.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 10,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "S", "Me gustaría cuidar personas con enfermedades mentales.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 11,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "S", "Me sentiría bien ayudando a las demás personas a comprenderse.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 12,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "S", "En mi futuro trabajo me gustaría ayudar a personas con discapacidades.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 13,
                columns: new[] { "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "Me las arreglo bien cuando tengo que organizar el trabajo de mis compañeros.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 14,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "E", "Me gusta tomar la palabra en diferentes discusiones y convencer a la gente.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 15,
                columns: new[] { "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "Me gustaría desempeñar la presidencia de mi clase.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 16,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "C", "Me gusta llevar mis cuadernos de manera ordenada y limpia.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 17,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "C", "Me gusta respetar y cumplir las fechas límites.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 18,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "C", "Me gusta organizar mi trabajo día a día y para la semana.", (byte)1 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 19,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "E", "Me considero el alma de la fiesta." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 20,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "E", "Prefiero no hablar mucho." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 21,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "E", "Me gusta iniciar conversaciones." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 22,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "E", "Suelo ser callado(a) cuando estoy con desconocidos." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 23,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "A", "Me intereso genuinamente por las personas." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 24,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "A", "A veces insulto o trato mal a la gente." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 25,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "A", "Tengo un corazón sensible." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 26,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "A", "No suelo interesarme mucho por los demás." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 27,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "C", "Siempre estoy preparado(a)." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 28,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "C", "Suelo dejar mis pertenencias tiradas o desordenadas." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 29,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "C", "Hago mis tareas de inmediato." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 30,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "C", "A veces evito o descuido mis obligaciones." });

            migrationBuilder.InsertData(
                table: "TBT_PREGUNTAS",
                columns: new[] { "IDD_PREGUNTA", "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[,]
                {
                    { 31, "N", "Normalmente me siento relajado(a).", (byte)2 },
                    { 32, "N", "Me preocupo demasiado por las cosas.", (byte)2 },
                    { 33, "N", "Rara vez me siento triste o deprimido(a).", (byte)2 },
                    { 34, "N", "Me altero o me enojo con facilidad.", (byte)2 },
                    { 35, "O", "Tengo un vocabulario rico y variado.", (byte)2 },
                    { 36, "O", "Me cuesta comprender ideas abstractas.", (byte)2 },
                    { 37, "O", "Suelo tener ideas excelentes o creativas.", (byte)2 },
                    { 38, "O", "No tengo mucha imaginación.", (byte)2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 38);

            migrationBuilder.UpdateData(
                table: "TBT_MODULOS",
                keyColumn: "IDD_MODULO",
                keyValue: (byte)2,
                columns: new[] { "COD_MODULO_TX", "NOM_MODULO_TX" },
                values: new object[] { "MBTI", "Test MBTI" });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 2,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "I", "El trabajo científico me parece muy interesante." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 3,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "A", "Sé tocar un instrumento musical o me gustaría aprender." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 4,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "S", "Me gustaría cuidar personas con enfermedades mentales." });

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
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "C", "Me gusta llevar mis cuadernos de manera ordenada y limpia." });

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
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "I", "Cuando paso tiempo solo: (A) Me aburro fácilmente / (B) Me siento recargado.", (byte)2 });

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
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "I", "Cuando aprendo algo nuevo: (A) Me gusta discutirlo con otros / (B) Prefiero analizarlo solo.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 13,
                columns: new[] { "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "Para relajarme: (A) Salgo con amigos / (B) Me quedo en casa leyendo o viendo algo.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 14,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "I", "En un viaje disfruto más: (A) Conocer mucha gente nueva / (B) Explorar a mi ritmo y en calma.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 15,
                columns: new[] { "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "Al resolver un problema: (A) Pienso en voz alta con otros / (B) Reflexiono internamente.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 16,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "I", "Cuando me hacen una pregunta: (A) Contesto de inmediato / (B) Pienso antes de responder.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 17,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "S", "Prefiero fijarme en: (A) Los hechos concretos / (B) Las posibilidades futuras.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 18,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[] { "N", "Cuando aprendo algo nuevo: (A) Necesito ejemplos prácticos / (B) Prefiero ideas y teorías generales.", (byte)2 });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 19,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "S", "Mi forma de trabajar es más: (A) Detallada y paso a paso / (B) Creativa y con saltos de ideas." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 20,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "N", "Al recordar algo tiendo a: (A) Enfocarme en los detalles / (B) Enfocarme en la idea principal." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 21,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "S", "En una conversación me gusta más: (A) Hablar de lo que está ocurriendo ahora / (B) Imaginar lo que podría ocurrir después." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 22,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "N", "Cuando sigo instrucciones: (A) Prefiero que sean claras y específicas / (B) Prefiero margen para interpretarlas." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 23,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "S", "Mis amigos dicen que soy: (A) Práctico y realista / (B) Soñador e imaginativo." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 24,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "N", "En un proyecto: (A) Me concentro en los pasos inmediatos / (B) Me concentro en el resultado final." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 25,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "S", "En clase me interesa más: (A) La aplicación práctica del tema / (B) La teoría y las conexiones." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 26,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "N", "En una historia: (A) Me fijo en los hechos / (B) Me fijo en el significado." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 27,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "T", "Al decidir: (A) Me baso en la lógica / (B) Me baso en lo que siento correcto." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 28,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "F", "Si un amigo comete un error: (A) Le digo directamente cómo mejorarlo / (B) Cuido sus sentimientos al corregirlo." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 29,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "J", "Prefiero: (A) Tener un plan definido / (B) Mantenerme flexible." });

            migrationBuilder.UpdateData(
                table: "TBT_PREGUNTAS",
                keyColumn: "IDD_PREGUNTA",
                keyValue: 30,
                columns: new[] { "COD_CATEGORIA", "DES_PREGUNTA_TX" },
                values: new object[] { "P", "Antes de un viaje: (A) Organizo horarios y actividades / (B) Veo qué surge sobre la marcha." });
        }
    }
}
