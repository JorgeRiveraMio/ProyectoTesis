namespace ProyectoTesis.Models;
using System;
public class TBD_ENVIO
{
    public int IDD_ENVIO { get; set; }
    public Guid IDD_RESULTADO { get; set; }
    public string DES_CORREO_TX { get; set; }
    public DateTime FEC_ENVIADO { get; set; }

    // Relaciones
    public TBM_RESULTADO RESULTADO { get; set; } 
}