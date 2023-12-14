using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class CarritoController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CarritoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarritoDto>> Get(int id)
    {
        var data = await unitOfWork.Carritos.GetByIdAsync(id);
        if (data == null)
        {
            return NotFound();
        }
        return mapper.Map<CarritoDto>(data);
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CarritoDto>>> Get()
    {
        var data = await unitOfWork.Carritos.GetAllAsync();
        return mapper.Map<List<CarritoDto>>(data);
    }

    [HttpGet("Pagination")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CarritoDto>>> GetPagination([FromQuery] Params dataParams)
    {
        var datos = await unitOfWork.Carritos.GetAllAsync(dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
        var listData = mapper.Map<List<CarritoDto>>(datos.registros);
        return new Pager<CarritoDto>(listData, datos.totalRegistros, dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarritoDto>> Post(CarritoDto dataDto)
    {
        var data = mapper.Map<Carrito>(dataDto);
        unitOfWork.Carritos.Add(data);
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
    public async Task<ActionResult<CarritoDto>> Put(int id, [FromBody] CarritoDto dataDto)
    {
        if (dataDto == null)
        {
            return NotFound();
        }
        var data = mapper.Map<Carrito>(dataDto);
        unitOfWork.Carritos.Update(data);
        await unitOfWork.SaveAsync();
        return dataDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var data = await unitOfWork.Carritos.GetByIdAsync(id);
        if (data == null)
        {
            return NotFound();
        }
        unitOfWork.Carritos.Remove(data);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}