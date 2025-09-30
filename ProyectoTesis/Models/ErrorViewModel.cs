namespace ProyectoTesis.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? ErrorMessage { get; set; }
        public Guid? SesionId { get; set; }
        public byte? ModuloId { get; set; }
        public Guid? IntentoId { get; set; }
    }
}
