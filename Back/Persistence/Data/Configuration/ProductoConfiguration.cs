using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Data.Configuration;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("Producto");

        builder.HasKey(e => e.Id);

        builder.Property(f => f.Id)
            .IsRequired();

        builder.Property(f => f.Nombre)
            .IsRequired()
            .HasColumnName("Nombre")
            .HasComment("Nombre del producto")
            .HasColumnType("varchar(30)")
            .HasMaxLength(30);

        builder.Property(f => f.Imagen)
            .IsRequired()
            .HasColumnName("Imagen")
            .HasColumnType("longblob");

        builder.Property(f => f.Cantidad)
            .IsRequired()
            .HasColumnName("Cantidad")
            .HasComment("Cantidad de producto");

        builder.Property(f => f.Precio)
            .IsRequired()
            .HasColumnName("Precio")
            .HasComment("Precio del producto")
            .HasColumnType("decimal(10,2)");

        builder.HasOne(p => p.Categoria)
            .WithMany(p => p.Productos)
            .HasForeignKey(p => p.IdCategoriaFK);
    }
}