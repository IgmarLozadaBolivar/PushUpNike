using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.Repository;

public class ProductoRepo : Generic<Producto>, IProducto
{
    protected readonly DbAppContext _context;

    public ProductoRepo(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
            //.Include(p => p.)
            .ToListAsync();
    }

    public override async Task<Producto> GetByIdAsync(string id)
    {
        return await _context.Productos
            //.Include(p => p.)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<(int totalRegistros, IEnumerable<Producto> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Productos as IQueryable<Producto>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Id.ToLower().Contains(search));
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
            query = query.Where(p => p.Imagen.ToString().ToLower().Contains(search));
            query = query.Where(p => p.Cantidad.ToString().ToLower().Contains(search));
            query = query.Where(p => p.IdCategoriaFK.ToString().ToLower().Contains(search));
            query = query.Where(p => p.Precio.ToString().ToLower().Contains(search));
        }
    
        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    
        return (totalRegistros, registros);
    }
}