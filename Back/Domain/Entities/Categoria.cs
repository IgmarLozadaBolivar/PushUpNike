namespace Domain.Entities;

public class Categoria
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public ICollection<Producto> Productos { get; set; }
}