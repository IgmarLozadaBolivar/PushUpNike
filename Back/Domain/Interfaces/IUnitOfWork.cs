namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IUser Users { get; }
    IRol Roles { get; }

    ICliente Clientes { get; }
    ICategoria Categorias { get; }
    IProducto Productos { get; }
    ICarrito Carritos { get; }

    Task<int> SaveAsync();
}