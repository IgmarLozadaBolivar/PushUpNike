namespace Domain.Entities;

public class Carrito
{
    public int Id { get; set; }
    public int IdClienteFK { get; set; }
    public Cliente Cliente { get; set; }
    public string IdProductoFK { get; set; }
    public Producto Producto { get; set; }
    public double Subtotal { get; set; }
    public double Total { get; set; }
}