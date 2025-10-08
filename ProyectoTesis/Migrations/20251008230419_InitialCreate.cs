using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoTesis.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBM_SESIONES",
                columns: table => new
                {
                    IDD_SESION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOM_ESTAD_SES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FEC_CREADO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FEC_INICIO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FEC_FIN = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBM_SESIONES", x => x.IDD_SESION);
                });

            migrationBuilder.CreateTable(
                name: "TBT_MODULOS",
                columns: table => new
                {
                    IDD_MODULO = table.Column<byte>(type: "tinyint", nullable: false),
                    COD_MODULO_TX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NOM_MODULO_TX = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBT_MODULOS", x => x.IDD_MODULO);
                });

            migrationBuilder.CreateTable(
                name: "TBL_EVENTOS",
                columns: table => new
                {
                    IDD_EVENTO = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDD_SESION = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TIP_EVENTO_TX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DES_DATOS_TX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FEC_CREADO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_EVENTOS", x => x.IDD_EVENTO);
                    table.ForeignKey(
                        name: "FK_TBL_EVENTOS_TBM_SESIONES_IDD_SESION",
                        column: x => x.IDD_SESION,
                        principalTable: "TBM_SESIONES",
                        principalColumn: "IDD_SESION");
                });

            migrationBuilder.CreateTable(
                name: "TBM_RESULTADOS",
                columns: table => new
                {
                    IDD_RESULTADO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDD_SESION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDD_PUBLICO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FEC_CREADO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NOM_PERFIL_TX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DES_RECOMENDACION_TX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_RECOMENDACIONES = table.Column<int>(type: "int", nullable: false),
                    LISTA_RECOMENDACIONES_JSON = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBM_RESULTADOS", x => x.IDD_RESULTADO);
                    table.ForeignKey(
                        name: "FK_TBM_RESULTADOS_TBM_SESIONES_IDD_SESION",
                        column: x => x.IDD_SESION,
                        principalTable: "TBM_SESIONES",
                        principalColumn: "IDD_SESION",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "TBM_INTENTOS",
                columns: table => new
                {
                    IDD_INTENTO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDD_SESION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDD_MODULO = table.Column<byte>(type: "tinyint", nullable: false),
                    FEC_INICIADO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FEC_COMPLETADO = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBM_INTENTOS", x => x.IDD_INTENTO);
                    table.ForeignKey(
                        name: "FK_TBM_INTENTOS_TBM_SESIONES_IDD_SESION",
                        column: x => x.IDD_SESION,
                        principalTable: "TBM_SESIONES",
                        principalColumn: "IDD_SESION",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBM_INTENTOS_TBT_MODULOS_IDD_MODULO",
                        column: x => x.IDD_MODULO,
                        principalTable: "TBT_MODULOS",
                        principalColumn: "IDD_MODULO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBT_PREGUNTAS",
                columns: table => new
                {
                    IDD_PREGUNTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDD_MODULO = table.Column<byte>(type: "tinyint", nullable: false),
                    DES_PREGUNTA_TX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_CATEGORIA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBT_PREGUNTAS", x => x.IDD_PREGUNTA);
                    table.ForeignKey(
                        name: "FK_TBT_PREGUNTAS_TBT_MODULOS_IDD_MODULO",
                        column: x => x.IDD_MODULO,
                        principalTable: "TBT_MODULOS",
                        principalColumn: "IDD_MODULO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBD_ENVIOS",
                columns: table => new
                {
                    IDD_ENVIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDD_RESULTADO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DES_CORREO_TX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FEC_ENVIADO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBD_ENVIOS", x => x.IDD_ENVIO);
                    table.ForeignKey(
                        name: "FK_TBD_ENVIOS_TBM_RESULTADOS_IDD_RESULTADO",
                        column: x => x.IDD_RESULTADO,
                        principalTable: "TBM_RESULTADOS",
                        principalColumn: "IDD_RESULTADO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBD_RESPUESTAS",
                columns: table => new
                {
                    IDD_REPOR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDD_INTENTO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDD_PREGUNTA = table.Column<int>(type: "int", nullable: false),
                    VAL_RESPUESTA_TX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FEC_GUARDADO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBD_RESPUESTAS", x => x.IDD_REPOR);
                    table.ForeignKey(
                        name: "FK_TBD_RESPUESTAS_TBM_INTENTOS_IDD_INTENTO",
                        column: x => x.IDD_INTENTO,
                        principalTable: "TBM_INTENTOS",
                        principalColumn: "IDD_INTENTO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBD_RESPUESTAS_TBT_PREGUNTAS_IDD_PREGUNTA",
                        column: x => x.IDD_PREGUNTA,
                        principalTable: "TBT_PREGUNTAS",
                        principalColumn: "IDD_PREGUNTA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TBT_MODULOS",
                columns: new[] { "IDD_MODULO", "COD_MODULO_TX", "NOM_MODULO_TX" },
                values: new object[,]
                {
                    { (byte)1, "RIASEC", "Test RIASEC" },
                    { (byte)2, "OCEAN", "Test Big Five (OCEAN)" }
                });

            migrationBuilder.InsertData(
                table: "TBT_PREGUNTAS",
                columns: new[] { "IDD_PREGUNTA", "COD_CATEGORIA", "DES_PREGUNTA_TX", "IDD_MODULO" },
                values: new object[,]
                {
                    { 1, "R", "Me gusta realizar pequeñas reparaciones de equipos electrodomésticos.", (byte)1 },
                    { 2, "R", "Me gustaría trabajar en el servicio técnico de una empresa.", (byte)1 },
                    { 3, "R", "Me interesa conocer el diseño y funcionamiento de los equipos técnicos.", (byte)1 },
                    { 4, "I", "El trabajo científico me parece muy interesante.", (byte)1 },
                    { 5, "I", "Me interesan los descubrimientos científicos y las nuevas invenciones.", (byte)1 },
                    { 6, "I", "Me gustaría realizar estudios y descubrir la vacuna contra una enfermedad grave.", (byte)1 },
                    { 7, "A", "Sé tocar un instrumento musical o me gustaría aprender.", (byte)1 },
                    { 8, "A", "Me gusta ver exposiciones de esculturas, pintura o fotografía.", (byte)1 },
                    { 9, "A", "Me gustaría expresarme mediante una actividad creativa como la pintura, el dibujo o la música.", (byte)1 },
                    { 10, "S", "Me gustaría cuidar personas con enfermedades mentales.", (byte)1 },
                    { 11, "S", "Me sentiría bien ayudando a las demás personas a comprenderse.", (byte)1 },
                    { 12, "S", "En mi futuro trabajo me gustaría ayudar a personas con discapacidades.", (byte)1 },
                    { 13, "E", "Me las arreglo bien cuando tengo que organizar el trabajo de mis compañeros.", (byte)1 },
                    { 14, "E", "Me gusta tomar la palabra en diferentes discusiones y convencer a la gente.", (byte)1 },
                    { 15, "E", "Me gustaría desempeñar la presidencia de mi clase.", (byte)1 },
                    { 16, "C", "Me gusta llevar mis cuadernos de manera ordenada y limpia.", (byte)1 },
                    { 17, "C", "Me gusta respetar y cumplir las fechas límites.", (byte)1 },
                    { 18, "C", "Me gusta organizar mi trabajo día a día y para la semana.", (byte)1 },
                    { 19, "E", "Me considero el alma de la fiesta.", (byte)2 },
                    { 20, "E", "Prefiero no hablar mucho.", (byte)2 },
                    { 21, "E", "Me gusta iniciar conversaciones.", (byte)2 },
                    { 22, "E", "Suelo ser callado(a) cuando estoy con desconocidos.", (byte)2 },
                    { 23, "A", "Me intereso genuinamente por las personas.", (byte)2 },
                    { 24, "A", "A veces insulto o trato mal a la gente.", (byte)2 },
                    { 25, "A", "Tengo un corazón sensible.", (byte)2 },
                    { 26, "A", "No suelo interesarme mucho por los demás.", (byte)2 },
                    { 27, "C", "Siempre estoy preparado(a).", (byte)2 },
                    { 28, "C", "Suelo dejar mis pertenencias tiradas o desordenadas.", (byte)2 },
                    { 29, "C", "Hago mis tareas de inmediato.", (byte)2 },
                    { 30, "C", "A veces evito o descuido mis obligaciones.", (byte)2 },
                    { 31, "N", "Normalmente me siento relajado(a).", (byte)2 },
                    { 32, "N", "Me preocupo demasiado por las cosas.", (byte)2 },
                    { 33, "N", "Rara vez me siento triste o deprimido(a).", (byte)2 },
                    { 34, "N", "Me altero o me enojo con facilidad.", (byte)2 },
                    { 35, "O", "Tengo un vocabulario rico y variado.", (byte)2 },
                    { 36, "O", "Me cuesta comprender ideas abstractas.", (byte)2 },
                    { 37, "O", "Suelo tener ideas excelentes o creativas.", (byte)2 },
                    { 38, "O", "No tengo mucha imaginación.", (byte)2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBD_ENVIOS_IDD_RESULTADO",
                table: "TBD_ENVIOS",
                column: "IDD_RESULTADO");

            migrationBuilder.CreateIndex(
                name: "IX_TBD_RESPUESTAS_IDD_INTENTO",
                table: "TBD_RESPUESTAS",
                column: "IDD_INTENTO");

            migrationBuilder.CreateIndex(
                name: "IX_TBD_RESPUESTAS_IDD_PREGUNTA",
                table: "TBD_RESPUESTAS",
                column: "IDD_PREGUNTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_EVENTOS_IDD_SESION",
                table: "TBL_EVENTOS",
                column: "IDD_SESION");

            migrationBuilder.CreateIndex(
                name: "IX_TBM_INTENTOS_IDD_MODULO",
                table: "TBM_INTENTOS",
                column: "IDD_MODULO");

            migrationBuilder.CreateIndex(
                name: "IX_TBM_INTENTOS_IDD_SESION",
                table: "TBM_INTENTOS",
                column: "IDD_SESION");

            migrationBuilder.CreateIndex(
                name: "IX_TBM_RESULTADOS_IDD_SESION",
                table: "TBM_RESULTADOS",
                column: "IDD_SESION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBM_SATISFACCIONES_IDD_SESION",
                table: "TBM_SATISFACCIONES",
                column: "IDD_SESION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBT_PREGUNTAS_IDD_MODULO",
                table: "TBT_PREGUNTAS",
                column: "IDD_MODULO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBD_ENVIOS");

            migrationBuilder.DropTable(
                name: "TBD_RESPUESTAS");

            migrationBuilder.DropTable(
                name: "TBL_EVENTOS");

            migrationBuilder.DropTable(
                name: "TBM_SATISFACCIONES");

            migrationBuilder.DropTable(
                name: "TBM_RESULTADOS");

            migrationBuilder.DropTable(
                name: "TBM_INTENTOS");

            migrationBuilder.DropTable(
                name: "TBT_PREGUNTAS");

            migrationBuilder.DropTable(
                name: "TBM_SESIONES");

            migrationBuilder.DropTable(
                name: "TBT_MODULOS");
        }
    }
}
