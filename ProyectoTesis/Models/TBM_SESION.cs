using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTesis.Models
{
    public class TBM_SESION
    {
        public Guid IDD_SESION { get; set; }
        public string NOM_ESTAD_SES { get; set; } = string.Empty;
        public DateTime FEC_CREADO { get; set; }

        public DateTime? FEC_INICIO { get; set; }
        public DateTime? FEC_FIN { get; set; }

        [NotMapped]
        public double DURACION_MINUTOS
        {
            get
            {
                if (FEC_INICIO.HasValue && FEC_FIN.HasValue)
                    return (FEC_FIN.Value - FEC_INICIO.Value).TotalMinutes;
                return 0;
            }
        }

        // Relaciones
        public ICollection<TBM_INTENTO> INTENTOS { get; set; } = new List<TBM_INTENTO>();
        public TBM_RESULTADO? RESULTADO { get; set; }
        public ICollection<TBL_EVENTO> EVENTOS { get; set; } = new List<TBL_EVENTO>();

        public TBM_SATISFACCION? SATISFACCION { get; set; }
    }
}
