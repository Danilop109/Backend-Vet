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
    // [Authorize]
    public class RazaController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RazaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RazaDto>>> Get()
    {
        var entidad = await _unitOfWork.Razas.GetAllAsync();
        return _mapper.Map<List<RazaDto>>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RazaDto>>> Get([FromQuery] Params Parameters)
    {
        var entidad = await _unitOfWork.Razas.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = _mapper.Map<List<RazaDto>>(entidad.registros);
        return Ok(new Pager<RazaDto>(listEntidad, entidad.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RazaDto>> Get(int id)
    {
        var entidad = await _unitOfWork.Razas.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<RazaDto>(entidad);
    }

    //CONSULTA B-6
    [HttpGet("GetPetsByRaza")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetPetsByRazaConsultaB6()
    {
        var entidad = await _unitOfWork.Razas.GetPetsByRaza();
        var dto = _mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("GetPetsByRaza")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> GetPetsByRazaConsultaB6([FromQuery] Params Parameters)
    {
        var entidad = await _unitOfWork.Razas.GetPetsByRaza(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = _mapper.Map<List<object>>(entidad.registros);
        return Ok(new Pager<object>(listEntidad, entidad.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Raza>> Post(RazaDto razaDto)
    {
        var entidad = this._mapper.Map<Raza>(razaDto);
        this._unitOfWork.Razas.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        razaDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = razaDto.Id}, razaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RazaDto>> Put(int id, [FromBody]RazaDto razaDto){
        if(razaDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<Raza>(razaDto);
        _unitOfWork.Razas.Update(entidad);
        await _unitOfWork.SaveAsync();
        return razaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.Razas.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.Razas.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}