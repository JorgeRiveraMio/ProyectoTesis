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

            builder.HasData(
                
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "R", DES_PREGUNTA_TX = "Me gusta realizar pequeñas reparaciones de equipos electrodomésticos." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "R", DES_PREGUNTA_TX = "Me gustaría trabajar en el servicio técnico de una empresa." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "R", DES_PREGUNTA_TX = "Me interesa conocer el diseño y funcionamiento de los equipos técnicos." },

                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "El trabajo científico me parece muy interesante." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "Me interesan los descubrimientos científicos y las nuevas invenciones." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "I", DES_PREGUNTA_TX = "Me gustaría realizar estudios y descubrir la vacuna contra una enfermedad grave." },

                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "A", DES_PREGUNTA_TX = "Sé tocar un instrumento musical o me gustaría aprender." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "A", DES_PREGUNTA_TX = "Me gusta ver exposiciones de esculturas, pintura o fotografía." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "A", DES_PREGUNTA_TX = "Me gustaría expresarme mediante una actividad creativa como la pintura, el dibujo o la música." },

                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "Me gustaría cuidar personas con enfermedades mentales." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "Me sentiría bien ayudando a las demás personas a comprenderse." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "S", DES_PREGUNTA_TX = "En mi futuro trabajo me gustaría ayudar a personas con discapacidades." },

                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Me las arreglo bien cuando tengo que organizar el trabajo de mis compañeros." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Me gusta tomar la palabra en diferentes discusiones y convencer a la gente." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Me gustaría desempeñar la presidencia de mi clase." },

                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "C", DES_PREGUNTA_TX = "Me gusta llevar mis cuadernos de manera ordenada y limpia." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "C", DES_PREGUNTA_TX = "Me gusta respetar y cumplir las fechas límites." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 1, COD_CATEGORIA = "C", DES_PREGUNTA_TX = "Me gusta organizar mi trabajo día a día y para la semana." }
            );

            
            builder.HasData(
                // Extraversion
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Me considero el alma de la fiesta." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Prefiero no hablar mucho." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Me gusta iniciar conversaciones." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "E", DES_PREGUNTA_TX = "Suelo ser callado(a) cuando estoy con desconocidos." },

                // Amabilidad
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "A", DES_PREGUNTA_TX = "Me intereso genuinamente por las personas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "A", DES_PREGUNTA_TX = "A veces insulto o trato mal a la gente." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "A", DES_PREGUNTA_TX = "Tengo un corazón sensible." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "A", DES_PREGUNTA_TX = "No suelo interesarme mucho por los demás." },

                // Responsabilidad
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "C", DES_PREGUNTA_TX = "Siempre estoy preparado(a)." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "C", DES_PREGUNTA_TX = "Suelo dejar mis pertenencias tiradas o desordenadas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "C", DES_PREGUNTA_TX = "Hago mis tareas de inmediato." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "C", DES_PREGUNTA_TX = "A veces evito o descuido mis obligaciones." },

                // Estabilidad Emocional
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "Normalmente me siento relajado(a)." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "Me preocupo demasiado por las cosas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "Rara vez me siento triste o deprimido(a)." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "N", DES_PREGUNTA_TX = "Me altero o me enojo con facilidad." },

                // Apertura
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "O", DES_PREGUNTA_TX = "Tengo un vocabulario rico y variado." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "O", DES_PREGUNTA_TX = "Me cuesta comprender ideas abstractas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "O", DES_PREGUNTA_TX = "Suelo tener ideas excelentes o creativas." },
                new TBT_PREGUNTA { IDD_PREGUNTA = id++, IDD_MODULO = 2, COD_CATEGORIA = "O", DES_PREGUNTA_TX = "No tengo mucha imaginación." }
            );

        }
    }
}
