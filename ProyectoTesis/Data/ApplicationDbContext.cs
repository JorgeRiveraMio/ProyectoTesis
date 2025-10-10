using Microsoft.EntityFrameworkCore;
using ProyectoTesis.Configurations;
using ProyectoTesis.Models;

namespace ProyectoTesis.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Tablas principales
        public DbSet<TBT_MODULO> TBT_MODULOS { get; set; }
        public DbSet<TBT_PREGUNTA> TBT_PREGUNTAS { get; set; }
        public DbSet<TBM_SESION> TBM_SESIONES { get; set; }
        public DbSet<TBM_INTENTO> TBM_INTENTOS { get; set; }
        public DbSet<TBM_RESULTADO> TBM_RESULTADOS { get; set; }
        public DbSet<TBD_RESPUESTA> TBD_RESPUESTAS { get; set; }
        public DbSet<TBD_ENVIO> TBD_ENVIOS { get; set; }
        public DbSet<TBL_EVENTO> TBL_EVENTOS { get; set; }
        public DbSet<TBM_SATISFACCION> TBM_SATISFACCIONES { get; set; }
        public DbSet<TBM_ESTUDIANTE> TBM_ESTUDIANTES { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TBT_MODULO>().ToTable("TBT_MODULOS");
            modelBuilder.Entity<TBT_PREGUNTA>().ToTable("TBT_PREGUNTAS");
            modelBuilder.Entity<TBM_SESION>().ToTable("TBM_SESIONES");
            modelBuilder.Entity<TBM_INTENTO>().ToTable("TBM_INTENTOS");
            modelBuilder.Entity<TBM_RESULTADO>().ToTable("TBM_RESULTADOS");
            modelBuilder.Entity<TBD_RESPUESTA>().ToTable("TBD_RESPUESTAS");
            modelBuilder.Entity<TBD_ENVIO>().ToTable("TBD_ENVIOS");
            modelBuilder.Entity<TBL_EVENTO>().ToTable("TBL_EVENTOS");
            modelBuilder.Entity<TBM_SATISFACCION>().ToTable("TBM_SATISFACCIONES");
            modelBuilder.Entity<TBM_ESTUDIANTE>().ToTable("TBM_ESTUDIANTES");

            modelBuilder.Entity<TBT_MODULO>().HasKey(m => m.IDD_MODULO);
            modelBuilder.Entity<TBT_PREGUNTA>().HasKey(p => p.IDD_PREGUNTA);
            modelBuilder.Entity<TBM_SESION>().HasKey(s => s.IDD_SESION);
            modelBuilder.Entity<TBM_INTENTO>().HasKey(i => i.IDD_INTENTO);
            modelBuilder.Entity<TBM_RESULTADO>().HasKey(r => r.IDD_RESULTADO);
            modelBuilder.Entity<TBD_RESPUESTA>().HasKey(r => r.IDD_REPOR);
            modelBuilder.Entity<TBD_ENVIO>().HasKey(e => e.IDD_ENVIO);
            modelBuilder.Entity<TBL_EVENTO>().HasKey(e => e.IDD_EVENTO);
            modelBuilder.Entity<TBM_SATISFACCION>().HasKey(s => s.IDD_SATISFACCION);
            modelBuilder.Entity<TBM_ESTUDIANTE>().HasKey(e => e.IDD_ESTUDIANTE); 

            // Relaciones principales
            modelBuilder.Entity<TBM_SESION>()
                .HasOne(s => s.RESULTADO)
                .WithOne(r => r.SESION)
                .HasForeignKey<TBM_RESULTADO>(r => r.IDD_SESION);

            modelBuilder.Entity<TBM_SESION>()
                .HasMany(s => s.INTENTOS)
                .WithOne(i => i.SESION)
                .HasForeignKey(i => i.IDD_SESION);

            modelBuilder.Entity<TBM_SESION>()
                .HasMany(s => s.EVENTOS)
                .WithOne(e => e.SESION)
                .HasForeignKey(e => e.IDD_SESION);

            // Relación 1:1 — Sesión ↔ Satisfacción
            modelBuilder.Entity<TBM_SESION>()
                .HasOne(s => s.SATISFACCION)
                .WithOne(sa => sa.SESION)
                .HasForeignKey<TBM_SATISFACCION>(sa => sa.IDD_SESION)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación 1:1 — Sesión ↔ Estudiante (nuevo)
            modelBuilder.Entity<TBM_SESION>()
                .HasOne<TBM_ESTUDIANTE>()
                .WithOne(e => e.SESION)
                .HasForeignKey<TBM_ESTUDIANTE>(e => e.IDD_SESION)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TBM_RESULTADO>()
                .HasMany(r => r.ENVIOS)
                .WithOne(e => e.RESULTADO)
                .HasForeignKey(e => e.IDD_RESULTADO);

            modelBuilder.Entity<TBM_INTENTO>()
                .HasMany(i => i.RESPUESTAS)
                .WithOne(r => r.INTENTO)
                .HasForeignKey(r => r.IDD_INTENTO)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TBT_PREGUNTA>()
                .HasMany(p => p.RESPUESTAS)
                .WithOne(r => r.PREGUNTA)
                .HasForeignKey(r => r.IDD_PREGUNTA)
                .OnDelete(DeleteBehavior.Restrict);

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
