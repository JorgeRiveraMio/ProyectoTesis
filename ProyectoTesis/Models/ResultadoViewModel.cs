using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoTesis.Models.ViewModels
{
    public class CarreraSugerida
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Icono { get; set; } = "school"; // valor por defecto
        public List<string> Universidades { get; set; } = new();

        // Puntaje combinado (RIASEC + OCEAN)
        public double Score { get; set; }
    }

    // Nuevo modelo auxiliar para el vector OCEAN
    public class OceanTrait
    {
        public string Trait { get; set; } = string.Empty;   // Ej: "O", "C", "E", "A", "N"
        public double Value { get; set; }                   // Valor numérico (0–5)
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

        // Subperfil RIASEC (ej. R-Tech, I-Science, etc.)
        public string Subperfil { get; set; } = string.Empty;

        // --- OCEAN (Big Five) ---
        public List<OceanTrait> PuntajesOcean { get; set; } = new();

        public string PerfilOceanResumen { get; set; } = string.Empty;

        // --- Integración ---
        public string PerfilCombinado =>
            !string.IsNullOrEmpty(PerfilRiasec) && !string.IsNullOrEmpty(Subperfil)
                ? $"{PerfilRiasec}-{Subperfil}"
                : !string.IsNullOrEmpty(PerfilRiasec)
                    ? PerfilRiasec
                    : "Perfil no calculado";

        // --- Carreras sugeridas ---
        public List<CarreraSugerida> Carreras { get; set; } = new();

        // --- Datos del estudiante ---
        public string EstudianteNombre { get; set; } = string.Empty;
        public int EstudianteEdad { get; set; }
        public string EstudianteGenero { get; set; } = string.Empty;
    }

}
