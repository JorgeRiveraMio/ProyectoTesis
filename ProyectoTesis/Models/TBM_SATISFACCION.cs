using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTesis.Models
{
    [Table("TBM_SATISFACCIONES")]
    public class TBM_SATISFACCION
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDD_SATISFACCION { get; set; }

        // Relación con la sesión (FK tipo Guid)
        [Required]
        public Guid IDD_SESION { get; set; }

        // --- Escala Likert (1–5) ---
        [Range(1, 5, ErrorMessage = "El valor debe estar entre 1 y 5.")]
        public int FACILIDAD_USO { get; set; }

        [Range(1, 5, ErrorMessage = "El valor debe estar entre 1 y 5.")]
        public int CLARIDAD_RESULTADOS { get; set; }

        [Range(1, 5, ErrorMessage = "El valor debe estar entre 1 y 5.")]
        public int UTILIDAD_RECOMENDACIONES { get; set; }

        [Range(1, 5, ErrorMessage = "El valor debe estar entre 1 y 5.")]
        public int SATISFACCION_GLOBAL { get; set; }

        // Fecha de registro automática
        [Required]
        public DateTime FEC_REGISTRO { get; set; } = DateTime.UtcNow;

        // --- Navegación a la sesión ---
        [ForeignKey(nameof(IDD_SESION))]
        public TBM_SESION? SESION { get; set; }   // Se deja nullable (?) para evitar advertencias CS8618
    }
}
