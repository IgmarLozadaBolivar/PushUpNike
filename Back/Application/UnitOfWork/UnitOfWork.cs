using Application.Repository;
using Domain.Interfaces;
using Persistence;
namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DbAppContext context;
    private UserRepo _users;
    private RolRepo _rols;
    private ClienteRepo _clientes;
    private CategoriaRepo _categorias;
    private ProductoRepo _productos;
    private CarritoRepo _carritos;

    public UnitOfWork(DbAppContext _context)
    {
        context = _context;
    }

    public IUser Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepo(context);
            }
            return _users;
        }
    }

    public IRol Roles
    {
        get
        {
            if (_rols == null)
            {
                _rols = new RolRepo(context);
            }
            return _rols;
        }
    }

    public ICliente Clientes
    {
        get
        {
            if (_clientes == null)
            {
                _clientes = new ClienteRepo(context);
            }
            return _clientes;
        }
    }

    public ICategoria Categorias
    {
        get
        {
            if (_categorias == null)
            {
                _categorias = new CategoriaRepo(context);
            }
            return _categorias;
        }
    }

    public IProducto Productos
    {
        get
        {
            if (_productos == null)
            {
                _productos = new ProductoRepo(context);
            }
            return _productos;
        }
    }

    public ICarrito Carritos
    {
        get
        {
            if (_carritos == null)
            {
                _carritos = new CarritoRepo(context);
            }
            return _carritos;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}