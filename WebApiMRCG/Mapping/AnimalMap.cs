using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMRCG.Entities;

namespace WebApiMRCG.Mapping
{
    public class AnimalMap : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(a => a.AnimalId);
            builder.Property(a => a.Descricao).HasMaxLength(300).IsRequired();
            builder.Property(a => a.Preco).IsRequired();

            builder.HasMany(a => a.CompraGadoItens).WithOne(a => a.Animal).HasForeignKey(a => a.AnimalId);

            builder.ToTable("Animal");
        }
    }
}
