using System;
using System.Collections.Generic;

namespace ProyectoTesis.Models.ViewModels
{
    public class CarreraSugerida
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; } // material-symbols-outlined
        public List<string> Universidades { get; set; } = new();
    }

    public class ResultadoViewModel
    {
        public Guid IDD_RESULTADO { get; set; }
        public string NOM_PERFIL_TX { get; set; }
        public string DES_RECOMENDACION_TX { get; set; }

        // RIASEC
        public Dictionary<string, int> Puntajes { get; set; } = new();
        public int Total { get; set; }
        public string PerfilRiasec { get; set; }

        // MBTI
        public string PerfilMbti { get; set; }
        public string DescripcionMbti { get; set; }

        // Integración
        public string PerfilCombinado => $"{PerfilRiasec} + {PerfilMbti}";

        // 🔹 Carreras sugeridas
        public List<CarreraSugerida> Carreras { get; set; } = new();
    }

}
