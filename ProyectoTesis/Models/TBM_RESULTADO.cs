namespace ProyectoTesis.Models;
using System;
using System.Collections.Generic;
public class TBM_RESULTADO
{
    public Guid IDD_RESULTADO { get; set; }
    public Guid IDD_SESION { get; set; }
    public Guid IDD_PUBLICO { get; set; }
    public DateTime FEC_CREADO { get; set; }
    public string NOM_PERFIL_TX { get; set; }
    public string DES_RECOMENDACION_TX { get; set; }

    // Relaciones
    public TBM_SESION SESION { get; set; }
    public ICollection<TBD_ENVIO> ENVIOS { get; set; } 
}