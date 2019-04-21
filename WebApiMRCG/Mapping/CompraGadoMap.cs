using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMRCG.Entities;

namespace WebApiMRCG.Mapping
{
    public class CompraGadoMap : IEntityTypeConfiguration<CompraGado>
    {
        public void Configure(EntityTypeBuilder<CompraGado> builder)
        {
            builder.HasKey(cg => cg.CompraGadoId);
            builder.Property(cg => cg.DataEntrega).IsRequired();

            builder.HasMany(cg => cg.CompraGadoItens).WithOne(cg => cg.CompraGado).HasForeignKey(cg => cg.CompraGadoId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(cg => cg.Pecuarista).WithMany(cg => cg.CompraGados).HasForeignKey(cg => cg.PecuaristaId);

            builder.ToTable("CompraGado");
        }
    }
}
