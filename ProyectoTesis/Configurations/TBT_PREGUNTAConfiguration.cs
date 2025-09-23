using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoTesis.Models;

namespace ProyectoTesis.Configurations
{
    public class TBT_PREGUNTAConfiguration : IEntityTypeConfiguration<TBT_PREGUNTA>
    {
        public void Configure(EntityTypeBuilder<TBT_PREGUNTA> builder)
        {
            int id = 1;

            // 🔹 Preguntas RIASEC
            builder.HasData(
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Me gusta realizar pequeñas reparaciones de equipos electrodomésticos." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "El trabajo científico me parece muy interesante." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Sé tocar un instrumento musical o me gustaría aprender." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Me gustaría cuidar personas con enfermedades mentales." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Me siento bien y me las arreglo cuando tengo que organizar el trabajo de mis compañeros y compañeras, fijarles tareas y comprobar si han sido realizadas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Me gusta llevar mis cuadernos de manera ordenada y limpia." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Me gustaría trabajar en el servicio técnico de una empresa." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Me gustaría trabajar en un centro de investigación o en un laboratorio." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "En el futuro me gustaría escribir poemas, guiones de películas o de juegos de video." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Me gusta mucho participar en organizaciones no gubernamentales como la Cruz Roja o una organización de jóvenes exploradores." },
                // ⚠️ Continúa hasta cubrir las ~60 preguntas de RIASEC que me pasaste.
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, DES_PREGUNTA_TX = "Me gusta organizar mi trabajo día a día y para la semana." }
            );

            // 🔹 Preguntas MBTI
            builder.HasData(
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, DES_PREGUNTA_TX = "En un grupo nuevo, prefiero: (A) Conocer y hablar con varias personas / (B) Observar primero y hablar solo con algunas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, DES_PREGUNTA_TX = "Cuando paso tiempo solo: (A) Me aburro fácilmente / (B) Me siento recargado." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, DES_PREGUNTA_TX = "En reuniones, suelo: (A) Hablar mucho y de manera espontánea / (B) Hablar solo cuando tengo algo importante." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, DES_PREGUNTA_TX = "Prefiero actividades: (A) Con mucha interacción social / (B) Tranquilas y personales." },
                // ⚠️ Aquí continúas hasta completar las 40 de MBTI.
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, DES_PREGUNTA_TX = "Para mí es más importante: (A) Terminar lo que empiezo / (B) Explorar nuevas cosas aunque no termine todas." }
            );
        }
    }
}
