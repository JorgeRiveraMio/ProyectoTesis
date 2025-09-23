using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Configurations;
using ProyectoTesis.Models; // Aquí estarán tus clases de entidades

namespace ProyectoTesis.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet = Representa una tabla de la BD
        public DbSet<TBT_MODULO> TBT_MODULOS { get; set; }
        public DbSet<TBT_PREGUNTA> TBT_PREGUNTAS { get; set; }
        public DbSet<TBM_SESION> TBM_SESIONES { get; set; }
        public DbSet<TBM_INTENTO> TBM_INTENTOS { get; set; }
        public DbSet<TBM_RESULTADO> TBM_RESULTADOS { get; set; }
        public DbSet<TBD_RESPUESTA> TBD_RESPUESTAS { get; set; }
        public DbSet<TBD_ENVIO> TBD_ENVIOS { get; set; }
        public DbSet<TBL_EVENTO> TBL_EVENTOS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Mapear nombres de tablas
            modelBuilder.Entity<TBT_MODULO>().ToTable("TBT_MODULOS");
            modelBuilder.Entity<TBT_PREGUNTA>().ToTable("TBT_PREGUNTAS");
            modelBuilder.Entity<TBM_SESION>().ToTable("TBM_SESIONES");
            modelBuilder.Entity<TBM_INTENTO>().ToTable("TBM_INTENTOS");
            modelBuilder.Entity<TBM_RESULTADO>().ToTable("TBM_RESULTADOS");
            modelBuilder.Entity<TBD_RESPUESTA>().ToTable("TBD_RESPUESTAS");
            modelBuilder.Entity<TBD_ENVIO>().ToTable("TBD_ENVIOS");
            modelBuilder.Entity<TBL_EVENTO>().ToTable("TBL_EVENTOS");

            // 🔹 PK explícitas
            modelBuilder.Entity<TBT_MODULO>().HasKey(m => m.IDD_MODULO);
            modelBuilder.Entity<TBT_PREGUNTA>().HasKey(p => p.IDD_PREGUNTA);
            modelBuilder.Entity<TBM_SESION>().HasKey(s => s.IDD_SESION);
            modelBuilder.Entity<TBM_INTENTO>().HasKey(i => i.IDD_INTENTO);
            modelBuilder.Entity<TBM_RESULTADO>().HasKey(r => r.IDD_RESULTADO);
            modelBuilder.Entity<TBD_RESPUESTA>().HasKey(r => r.IDD_REPOR);
            modelBuilder.Entity<TBD_ENVIO>().HasKey(e => e.IDD_ENVIO);
            modelBuilder.Entity<TBL_EVENTO>().HasKey(e => e.IDD_EVENTO);

            // 🔹 Relación 1:1 Sesion - Resultado
            modelBuilder.Entity<TBM_SESION>()
                .HasOne(s => s.RESULTADO)
                .WithOne(r => r.SESION)
                .HasForeignKey<TBM_RESULTADO>(r => r.IDD_SESION);

            // 🔹 Relación 1:N Sesion - Intentos
            modelBuilder.Entity<TBM_SESION>()
                .HasMany(s => s.INTENTOS)
                .WithOne(i => i.SESION)
                .HasForeignKey(i => i.IDD_SESION);

            // 🔹 Relación 1:N Sesion - Eventos
            modelBuilder.Entity<TBM_SESION>()
                .HasMany(s => s.EVENTOS)
                .WithOne(e => e.SESION)
                .HasForeignKey(e => e.IDD_SESION);

            // 🔹 Relación 1:N Resultado - Envíos
            modelBuilder.Entity<TBM_RESULTADO>()
                .HasMany(r => r.ENVIOS)
                .WithOne(e => e.RESULTADO)
                .HasForeignKey(e => e.IDD_RESULTADO);

            // 🔹 Relación 1:N Intento - Respuestas (con cascada)
            modelBuilder.Entity<TBM_INTENTO>()
                .HasMany(i => i.RESPUESTAS)
                .WithOne(r => r.INTENTO)
                .HasForeignKey(r => r.IDD_INTENTO)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Relación 1:N Pregunta - Respuestas (sin cascada para evitar ciclos)
            modelBuilder.Entity<TBT_PREGUNTA>()
                .HasMany(p => p.RESPUESTAS)
                .WithOne(r => r.PREGUNTA)
                .HasForeignKey(r => r.IDD_PREGUNTA)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Relación Pregunta - Módulo (explicita para evitar MODULOIDD_MODULO)
            modelBuilder.Entity<TBT_PREGUNTA>()
                .HasOne(p => p.MODULO)
                .WithMany(m => m.PREGUNTAS)
                .HasForeignKey(p => p.IDD_MODULO)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfiguration(new TBT_MODULOConfiguration());
            modelBuilder.ApplyConfiguration(new TBT_PREGUNTAConfiguration());


        }
    }
}
