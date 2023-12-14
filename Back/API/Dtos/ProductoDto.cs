namespace API.Dtos;

public class ProductoDto
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public byte[] Imagen { get; set; }
    public int Cantidad { get; set; }
    public int IdCategoriaFK { get; set; }
    public CategoriaDto categorias { get; set; }
    public double Precio { get; set; }
}