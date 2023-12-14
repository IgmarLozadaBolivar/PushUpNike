using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Data.Configuration;

public class CarritoConfiguration : IEntityTypeConfiguration<Carrito>
{
    public void Configure(EntityTypeBuilder<Carrito> builder)
    {
        builder.ToTable("Carrito");

        builder.HasKey(e => e.Id);

        builder.Property(f => f.Id)
            .IsRequired();

        builder.Property(f => f.Subtotal)
            .IsRequired()
            .HasColumnName("Subtotal")
            .HasComment("Subtotal del producto")
            .HasColumnType("decimal(10,2)");

        builder.Property(f => f.Total)
            .IsRequired()
            .HasColumnName("Total")
            .HasComment("Total del carrito")
            .HasColumnType("decimal(10,2)");

        builder.HasOne(p => p.Cliente)
            .WithMany(p => p.Carritos)
            .HasForeignKey(p => p.IdClienteFK);

        builder.HasOne(p => p.Producto)
            .WithMany(p => p.Carritos)
            .HasForeignKey(p => p.IdProductoFK);
    }
}