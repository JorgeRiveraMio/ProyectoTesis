namespace ProyectoTesis.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // 🔹 Nuevos campos para debug
        public string? ErrorMessage { get; set; }
        public Guid? SesionId { get; set; }
        public byte? ModuloId { get; set; }
        public Guid? IntentoId { get; set; }
    }
}
