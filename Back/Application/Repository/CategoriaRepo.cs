using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.Repository;

public class CategoriaRepo : Generic<Categoria>, ICategoria
{
    protected readonly DbAppContext _context;

    public CategoriaRepo(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categorias
            //.Include(p => p.)
            .ToListAsync();
    }

    public override async Task<Categoria> GetByIdAsync(int id)
    {
        return await _context.Categorias
            //.Include(p => p.)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<(int totalRegistros, IEnumerable<Categoria> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Categorias as IQueryable<Categoria>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Id.ToString().ToLower().Contains(search));
            query = query.Where(p => p.Descripcion.ToLower().Contains(search));
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