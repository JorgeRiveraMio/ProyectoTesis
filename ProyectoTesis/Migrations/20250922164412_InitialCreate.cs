using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    FEC_CREADO = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DES_RECOMENDACION_TX = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "TBM_INTENTOS",
                columns: table => new
                {
                    IDD_INTENTO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDD_SESION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDD_MODULO = table.Column<byte>(type: "tinyint", nullable: false),
                    FEC_INICIADO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FEC_COMPLETADO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODULOIDD_MODULO = table.Column<byte>(type: "tinyint", nullable: false)
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
                        name: "FK_TBM_INTENTOS_TBT_MODULOS_MODULOIDD_MODULO",
                        column: x => x.MODULOIDD_MODULO,
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
                    MODULOIDD_MODULO = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBT_PREGUNTAS", x => x.IDD_PREGUNTA);
                    table.ForeignKey(
                        name: "FK_TBT_PREGUNTAS_TBT_MODULOS_MODULOIDD_MODULO",
                        column: x => x.MODULOIDD_MODULO,
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
                name: "IX_TBM_INTENTOS_IDD_SESION",
                table: "TBM_INTENTOS",
                column: "IDD_SESION");

            migrationBuilder.CreateIndex(
                name: "IX_TBM_INTENTOS_MODULOIDD_MODULO",
                table: "TBM_INTENTOS",
                column: "MODULOIDD_MODULO");

            migrationBuilder.CreateIndex(
                name: "IX_TBM_RESULTADOS_IDD_SESION",
                table: "TBM_RESULTADOS",
                column: "IDD_SESION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBT_PREGUNTAS_MODULOIDD_MODULO",
                table: "TBT_PREGUNTAS",
                column: "MODULOIDD_MODULO");
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
