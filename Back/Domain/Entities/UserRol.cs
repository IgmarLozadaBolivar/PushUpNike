namespace Domain.Entities;

public class UserRol
{
    public int IdUserFK { get; set; }
    public User User { get; set; }
    public int IdRolFK { get; set; }
    public Rol Rol { get; set; }
}