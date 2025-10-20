using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyectoTesis.Models; 

namespace ProyectoTesis.Models
{
    public class TBM_ESTUDIANTE
    {
        [Key]
        public Guid IDD_ESTUDIANTE { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        [Display(Name = "Nombre completo")]
        public string NOM_COMPLETO { get; set; } = string.Empty;

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [Range(12, 100, ErrorMessage = "La edad debe ser mayor o igual a 12 años.")]
        [Display(Name = "Edad")]
        public int NUM_EDAD { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un género.")]
        [StringLength(20)]
        [Display(Name = "Género")]
        public string NOM_GENERO { get; set; } = string.Empty;

        [Required]
        public Guid IDD_SESION { get; set; }

        [ForeignKey(nameof(IDD_SESION))]
        public TBM_SESION? SESION { get; set; }
    }
}
