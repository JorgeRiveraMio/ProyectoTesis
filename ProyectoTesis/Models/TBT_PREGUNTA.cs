namespace ProyectoTesis.Models;
using System.Collections.Generic;
public class TBT_PREGUNTA
{
    public int IDD_PREGUNTA { get; set; }
    public byte IDD_MODULO { get; set; }
    public string DES_PREGUNTA_TX { get; set; }

    // Relaciones
    public TBT_MODULO MODULO { get; set; }
    public ICollection<TBD_RESPUESTA> RESPUESTAS { get; set; } 
}