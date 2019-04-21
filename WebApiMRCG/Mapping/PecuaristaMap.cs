using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMRCG.Entities;

namespace WebApiMRCG.Mapping
{
    public class PecuaristaMap : IEntityTypeConfiguration<Pecuarista>
    {
        public void Configure(EntityTypeBuilder<Pecuarista> builder)
        {
            builder.HasKey(p => p.PecuaristaId);
            builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();

            builder.HasMany(p => p.CompraGados).WithOne(p => p.Pecuarista).HasForeignKey(p => p.PecuaristaId);

            builder.ToTable("Pecuarista");
        }
    }
}
