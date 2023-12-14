namespace Domain.Entities;

public class Producto
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public byte[] Imagen { get; set; }
    public int IdCategoriaFK { get; set; }
    public Categoria Categoria { get; set; }
    public double Precio { get; set; }
    public ICollection<Carrito> Carritos { get; set; }
}