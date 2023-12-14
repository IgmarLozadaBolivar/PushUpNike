using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.Repository;

public class CarritoRepo : Generic<Carrito>, ICarrito
{
    protected readonly DbAppContext _context;

    public CarritoRepo(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Carrito>> GetAllAsync()
    {
        return await _context.Carritos
            //.Include(p => p.)
            .ToListAsync();
    }

    public override async Task<Carrito> GetByIdAsync(int id)
    {
        return await _context.Carritos
            //.Include(p => p.)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<(int totalRegistros, IEnumerable<Carrito> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Carritos as IQueryable<Carrito>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Id.ToString().ToLower().Contains(search));
            query = query.Where(p => p.IdClienteFK.ToString().ToLower().Contains(search));
            query = query.Where(p => p.IdProductoFK.ToString().ToLower().Contains(search));
            query = query.Where(p => p.Subtotal.ToString().ToLower().Contains(search));
            query = query.Where(p => p.Total.ToString().ToLower().Contains(search));
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