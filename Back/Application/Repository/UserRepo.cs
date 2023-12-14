using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.Repository;

public class UserRepo : Generic<User>, IUser
{
    protected readonly DbAppContext _context;

    public UserRepo(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            //.Include(p => p.)
            .ToListAsync();
    }

    public override async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users
            //.Include(p => p.)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<User> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
                .Include(u => u.Rols)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Rols)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Nombre.ToLower() == username.ToLower());
    }

    public override async Task<(int totalRegistros, IEnumerable<User> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Users as IQueryable<User>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Id.ToString().ToLower().Contains(search));
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
            query = query.Where(p => p.Email.ToLower().Contains(search));
            query = query.Where(p => p.Password.ToLower().Contains(search));
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