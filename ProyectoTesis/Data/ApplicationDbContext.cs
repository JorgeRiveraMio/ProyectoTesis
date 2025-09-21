using Microsoft.EntityFrameworkCore;

namespace ProyectoTesis.Data
{
    // Clase que representa el "puente" entre tu aplicación y la base de datos
    // Aquí defines cómo se van a mapear tus entidades (clases) a tablas o vistas de la base de datos
    public class ApplicationDbContext : DbContext
    {
        // Constructor del contexto
        // Recibe las opciones de configuración (cadena de conexión, proveedor de base de datos, etc.)
        // Estas opciones se pasan desde el Program.cs o Startup.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Aquí defines los DbSet que representan tus tablas en la BD
        // Ejemplo:
        // public DbSet<Usuario> Usuarios { get; set; }
        // public DbSet<Producto> Productos { get; set; }

        // Método usado para configurar el mapeo entre tus clases (entidades) y la base de datos
        // Sirve para personalizar:
        //   - Nombres de tablas
        //   - Claves primarias y foráneas
        //   - Propiedades ignoradas
        //   - Configurar vistas
        //   - Relaciones (1 a 1, 1 a muchos, muchos a muchos)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Muy importante: llamar primero al base.OnModelCreating
            // Esto asegura que se apliquen configuraciones predeterminadas de EF Core
            base.OnModelCreating(modelBuilder);

            // Ejemplo: cambiar el nombre de una tabla
            // modelBuilder.Entity<Usuario>().ToTable("TB_USUARIOS");

            // Ejemplo: definir clave compuesta
            // modelBuilder.Entity<Pedido>().HasKey(p => new { p.PedidoId, p.ProductoId });

            // Ejemplo: ignorar una propiedad que no quieres guardar en la BD
            // modelBuilder.Entity<Usuario>().Ignore(u => u.PropiedadTemporal);
        }


    }
}
