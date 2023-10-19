using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers.Errors;
using API.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Authorize]
    public class PropietarioController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropietarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PropietarioDto>>> Get()
    {
        var entidad = await _unitOfWork.Propietarios.GetAllAsync();
        return _mapper.Map<List<PropietarioDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PropietarioDto>> Get(int id)
    {
        var entidad = await _unitOfWork.Propietarios.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<PropietarioDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PropietarioDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await _unitOfWork.Propietarios.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = _mapper.Map<List<PropietarioDto>>(entidad.registros);
        return new Pager<PropietarioDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }

    //CONSULTA 4
    [HttpGet("GetPetPer")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetPetPerConsulta4()
    {
        var entidad = await _unitOfWork.Propietarios.GetPetPer();
        var dto = _mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("GetPetPer")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> GetPetPerConsulta4Pag([FromQuery] Params Parameters)
    {
        var entidad = await _unitOfWork.Propietarios.GetPetPer(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = _mapper.Map<List<object>>(entidad.registros);
        return Ok(new Pager<object>(listEntidad, entidad.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Propietario>> Post(PropietarioDto propietarioDto)
    {
        var entidad = this._mapper.Map<Propietario>(propietarioDto);
        this._unitOfWork.Propietarios.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        propietarioDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = propietarioDto.Id}, propietarioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PropietarioDto>> Put(int id, [FromBody]PropietarioDto propietarioDto){
        if(propietarioDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<Propietario>(propietarioDto);
        _unitOfWork.Propietarios.Update(entidad);
        await _unitOfWork.SaveAsync();
        return propietarioDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.Propietarios.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.Propietarios.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}