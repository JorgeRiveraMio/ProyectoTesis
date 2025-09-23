namespace ProyectoTesis.Models;
using System;
public class TBD_RESPUESTA
{
    public int IDD_REPOR { get; set; }
    public Guid IDD_INTENTO { get; set; }
    public int IDD_PREGUNTA { get; set; }
    public string VAL_RESPUESTA_TX { get; set; }
    public DateTime FEC_GUARDADO { get; set; }

    // Relaciones
    public TBM_INTENTO INTENTO { get; set; }
    public TBT_PREGUNTA PREGUNTA { get; set; } 
}