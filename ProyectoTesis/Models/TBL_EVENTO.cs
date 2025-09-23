namespace ProyectoTesis.Models;
using System;
public class TBL_EVENTO
{
    public long IDD_EVENTO { get; set; }
    public Guid? IDD_SESION { get; set; }
    public string TIP_EVENTO_TX { get; set; }
    public string DES_DATOS_TX { get; set; }
    public DateTime FEC_CREADO { get; set; }

    // Relaciones
    public TBM_SESION SESION { get; set; } 
}