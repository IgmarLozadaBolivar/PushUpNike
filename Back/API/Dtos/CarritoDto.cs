namespace API.Dtos;

public class CarritoDto
{
    public int Id { get; set; }
    public int IdClienteFK { get; set; }
    public ClienteDto clientes { get; }
    public int IdProductoFK { get; set; }
    public ProductoDto productos { get; }
    public double Subtotal { get; set; }
    public double Total { get; set; }
}