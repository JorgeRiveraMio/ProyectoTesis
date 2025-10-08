using System;
using System.Collections.Generic;

namespace ProyectoTesis.Models.ViewModels
{
    public class CarreraSugerida
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Icono { get; set; } = "school"; // valor por defecto
        public List<string> Universidades { get; set; } = new();
        
        //  Nuevo: puntaje de afinidad (RIASEC + OCEAN)
        public double Score { get; set; } 
    }

    public class ResultadoViewModel
    {
        public Guid IDD_RESULTADO { get; set; }

        public string NOM_PERFIL_TX { get; set; } = string.Empty;
        public string DES_RECOMENDACION_TX { get; set; } = string.Empty;

        // --- RIASEC ---
        public Dictionary<string, int> PuntajesRiasec { get; set; } = new();  
        public int TotalRiasec { get; set; }
        public string PerfilRiasec { get; set; } = string.Empty;

        //  Nuevo: subperfil RIASEC (ej. R-Tech, I-Science, etc.)
        public string Subperfil { get; set; } = string.Empty;

        // --- OCEAN (Big Five) ---
        public Dictionary<string, double> PuntajesOcean { get; set; } = new();
        public string PerfilOceanResumen { get; set; } = string.Empty;

        // --- Integración ---
        public string PerfilCombinado =>
            !string.IsNullOrEmpty(PerfilRiasec) && !string.IsNullOrEmpty(Subperfil)
                ? $"{PerfilRiasec}-{Subperfil}"
                : !string.IsNullOrEmpty(PerfilRiasec) ? PerfilRiasec
                : "Perfil no calculado";

        // --- Carreras sugeridas ---
        public List<CarreraSugerida> Carreras { get; set; } = new();
    }
}
