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

            // 🔹 Preguntas RIASEC (ejemplo, deberías completar todas las ~60)
            builder.HasData(
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "R", DES_PREGUNTA_TX = "Me gusta realizar pequeñas reparaciones de equipos electrodomésticos." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "El trabajo científico me parece muy interesante." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "A", DES_PREGUNTA_TX = "Sé tocar un instrumento musical o me gustaría aprender." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "Me gustaría cuidar personas con enfermedades mentales." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Me las arreglo bien cuando tengo que organizar el trabajo de mis compañeros, fijarles tareas y comprobar si han sido realizadas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "C", DES_PREGUNTA_TX = "Me gusta llevar mis cuadernos de manera ordenada y limpia." }
                // ⚠️ Completar hasta las ~60 preguntas que ya listaste
            );

            // 🔹 Preguntas MBTI (40 ítems, con COD_CATEGORIA asignado según dimensión)
            // Grupo 1: Extroversión (E) vs Introversión (I) -> Preguntas 1-10
            builder.HasData(
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "En un grupo nuevo, prefiero: (A) Conocer y hablar con varias personas / (B) Observar primero y hablar solo con algunas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "Cuando paso tiempo solo: (A) Me aburro fácilmente / (B) Me siento recargado." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "En reuniones, suelo: (A) Hablar mucho y de manera espontánea / (B) Hablar solo cuando tengo algo importante." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "Prefiero actividades: (A) Con mucha interacción social / (B) Tranquilas y personales." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Mis amigos me describen como: (A) Energético y sociable / (B) Reservado y reflexivo." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "Cuando aprendo algo nuevo: (A) Me gusta discutirlo con otros / (B) Prefiero analizarlo solo." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Para relajarme: (A) Salgo con amigos / (B) Me quedo en casa leyendo o viendo algo." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "En un viaje disfruto más: (A) Conocer mucha gente nueva / (B) Explorar a mi ritmo y en calma." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Al resolver un problema: (A) Pienso en voz alta con otros / (B) Reflexiono internamente." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "Cuando me hacen una pregunta: (A) Contesto de inmediato / (B) Pienso antes de responder." }
            );

            // Grupo 2: Sensación (S) vs Intuición (N) -> Preguntas 11-20
            builder.HasData(
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "Prefiero fijarme en: (A) Los hechos concretos / (B) Las posibilidades futuras." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "Cuando aprendo algo nuevo: (A) Necesito ejemplos prácticos / (B) Prefiero ideas y teorías generales." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "Mi forma de trabajar es más: (A) Detallada y paso a paso / (B) Creativa y con saltos de ideas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "Al recordar algo tiendo a: (A) Enfocarme en los detalles / (B) Enfocarme en la idea principal." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "En una conversación me gusta más: (A) Hablar de lo que está ocurriendo ahora / (B) Imaginar lo que podría ocurrir después." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "Cuando sigo instrucciones: (A) Prefiero que sean claras y específicas / (B) Prefiero margen para interpretarlas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "Mis amigos dicen que soy: (A) Práctico y realista / (B) Soñador e imaginativo." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "En un proyecto: (A) Me concentro en los pasos inmediatos / (B) Me concentro en el resultado final." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "En clase me interesa más: (A) La aplicación práctica del tema / (B) La teoría y las conexiones." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "En una historia: (A) Me fijo en los hechos / (B) Me fijo en el significado." }
            );

            // Grupo 3: Pensamiento (T) vs Sentimiento (F) -> Preguntas 21-30
            builder.HasData(
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "T", DES_PREGUNTA_TX = "Al decidir: (A) Me baso en la lógica / (B) Me baso en lo que siento correcto." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "F", DES_PREGUNTA_TX = "Si un amigo comete un error: (A) Le digo directamente cómo mejorarlo / (B) Cuido sus sentimientos al corregirlo." }
                // ⚠️ Y así hasta completar las 10 de T/F...
            );

            // Grupo 4: Juicio (J) vs Percepción (P) -> Preguntas 31-40
            builder.HasData(
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "J", DES_PREGUNTA_TX = "Prefiero: (A) Tener un plan definido / (B) Mantenerme flexible." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "P", DES_PREGUNTA_TX = "Antes de un viaje: (A) Organizo horarios y actividades / (B) Veo qué surge sobre la marcha." }
                // ⚠️ Igual deberías completar hasta la 40
            );
        }
    }
}
