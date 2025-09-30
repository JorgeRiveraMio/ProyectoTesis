using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoTesis.Models;

namespace ProyectoTesis.Configurations
{
    public class TBT_MODULOConfiguration : IEntityTypeConfiguration<TBT_MODULO>
    {
        public void Configure(EntityTypeBuilder<TBT_MODULO> builder)
        {
            builder.HasData(
                new TBT_MODULO { IDD_MODULO = 1, COD_MODULO_TX = "RIASEC", NOM_MODULO_TX = "Test RIASEC" },
                new TBT_MODULO { IDD_MODULO = 2, COD_MODULO_TX = "OCEAN", NOM_MODULO_TX = "Test Big Five (OCEAN)" }
            );

        }
    }
}