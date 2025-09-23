namespace ProyectoTesis.Models;
using System;
using System.Collections.Generic;
public class TBM_SESION
{
    public Guid IDD_SESION { get; set; }
    public string NOM_ESTAD_SES { get; set; }
    public DateTime FEC_CREADO { get; set; }

    // Relaciones
    public ICollection<TBM_INTENTO> INTENTOS { get; set; }
    public TBM_RESULTADO RESULTADO { get; set; }
    public ICollection<TBL_EVENTO> EVENTOS { get; set; } 
}