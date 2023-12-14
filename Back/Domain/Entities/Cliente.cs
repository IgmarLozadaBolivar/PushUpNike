namespace Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public ICollection<Carrito> Carritos { get; set; }
}