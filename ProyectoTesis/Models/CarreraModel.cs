using System.Collections.Generic;

namespace ProyectoTesis.Models
{
    public class Carrera
    {
        public string carrera { get; set; }
        public string descripcion { get; set; }
        public List<string> universidades { get; set; }
    }

    public class PerfilRIASEC
    {
        public Dictionary<string, List<Carrera>> R { get; set; }
        public Dictionary<string, List<Carrera>> I { get; set; }
        public Dictionary<string, List<Carrera>> A { get; set; }
        public Dictionary<string, List<Carrera>> S { get; set; }
        public Dictionary<string, List<Carrera>> E { get; set; }
        public Dictionary<string, List<Carrera>> C { get; set; }
    }
}
