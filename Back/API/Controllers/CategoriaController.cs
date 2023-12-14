using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class CategoriaController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CategoriaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Get(int id)
    {
        var data = await unitOfWork.Categorias.GetByIdAsync(id);
        if (data == null)
        {
            return NotFound();
        }
        return mapper.Map<CategoriaDto>(data);
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> Get()
    {
        var data = await unitOfWork.Categorias.GetAllAsync();
        return mapper.Map<List<CategoriaDto>>(data);
    }

    [HttpGet("Pagination")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CategoriaDto>>> GetPagination([FromQuery] Params dataParams)
    {
        var datos = await unitOfWork.Categorias.GetAllAsync(dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
        var listData = mapper.Map<List<CategoriaDto>>(datos.registros);
        return new Pager<CategoriaDto>(listData, datos.totalRegistros, dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Post(CategoriaDto dataDto)
    {
        var data = mapper.Map<Categoria>(dataDto);
        unitOfWork.Categorias.Add(data);
        await unitOfWork.SaveAsync();
        if (data == null)
        {
            return BadRequest();
        }
        dataDto.Id = data.Id;
        return CreatedAtAction(nameof(Post), new { id = dataDto.Id }, dataDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDto>> Put(int id, [FromBody] CategoriaDto dataDto)
    {
        if (dataDto == null)
        {
            return NotFound();
        }
        var data = mapper.Map<Categoria>(dataDto);
        unitOfWork.Categorias.Update(data);
        await unitOfWork.SaveAsync();
        return dataDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var data = await unitOfWork.Categorias.GetByIdAsync(id);
        if (data == null)
        {
            return NotFound();
        }
        unitOfWork.Categorias.Remove(data);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}