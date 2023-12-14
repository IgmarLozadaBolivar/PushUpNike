using Domain.Entities;
namespace Domain.Interfaces;

public interface IUser : IGeneric<User>
{
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByRefreshTokenAsync(string username);
}