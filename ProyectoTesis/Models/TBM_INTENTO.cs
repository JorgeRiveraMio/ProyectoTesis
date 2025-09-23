namespace ProyectoTesis.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class TBM_INTENTO
{
    public Guid IDD_INTENTO { get; set; }
    public Guid IDD_SESION { get; set; }

    public byte IDD_MODULO { get; set; }

    public DateTime FEC_INICIADO { get; set; }
    public DateTime? FEC_COMPLETADO { get; set; }

    // Relaciones
    public TBM_SESION SESION { get; set; }

    [ForeignKey(nameof(IDD_MODULO))]
    public TBT_MODULO MODULO { get; set; }

    public ICollection<TBD_RESPUESTA> RESPUESTAS { get; set; } 
}