namespace ProyectoTesis.Models;
using System.Collections.Generic;
public class TBT_MODULO
{
    public byte IDD_MODULO { get; set; }
    public string COD_MODULO_TX { get; set; }
    public string NOM_MODULO_TX { get; set; }

    // Relaciones
    public ICollection<TBT_PREGUNTA> PREGUNTAS { get; set; }
    public ICollection<TBM_INTENTO> INTENTOS { get; set; } 
    
    
}