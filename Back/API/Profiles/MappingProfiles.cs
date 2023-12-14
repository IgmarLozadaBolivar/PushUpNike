using API.Dtos;
using AutoMapper;
using Domain.Entities;
namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rol, RolDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<Cliente, ClienteDto>().ReverseMap();
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
        CreateMap<Producto, ProductoDto>().ReverseMap();
        CreateMap<Carrito, CarritoDto>().ReverseMap();
    }
}