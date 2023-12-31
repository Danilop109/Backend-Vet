using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Helpers.Errors;
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
    public class MedicamentoProveedorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicamentoProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MedicamentoProveedorDto>>> Get()
    {
        var entidad = await _unitOfWork.MedicamentoProveedores.GetAllAsync();
        return _mapper.Map<List<MedicamentoProveedorDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MedicamentoProveedorDto>> Get(int id)
    {
        var entidad = await _unitOfWork.MedicamentoProveedores.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<MedicamentoProveedorDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MedicamentoProveedorDto>>> Get([FromQuery] Params Parameters)
    {
        var entidad = await _unitOfWork.MedicamentoProveedores.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = _mapper.Map<List<MedicamentoProveedorDto>>(entidad.registros);
        return Ok(new Pager<MedicamentoProveedorDto>(listEntidad, entidad.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));
    }

    //CONSULTA B-4
    [HttpGet("GetProveeSaleMedi")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetProveeSaleMediConsultaB4()
    {
        var entidad = await _unitOfWork.MedicamentoProveedores.GetProveeSaleMedi();
        var dto = _mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("GetProveeSaleMedi")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> GetProveeSaleMediConsultaB4Pag([FromQuery] Params Parameters)
    {
        var entidad = await _unitOfWork.MedicamentoProveedores.GetProveeSaleMedi(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = _mapper.Map<List<object>>(entidad.registros);
        return Ok(new Pager<object>(listEntidad, entidad.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MedicamentoProveedor>> Post(MedicamentoProveedorDto medicamentoProveedorDto)
    {
        var entidad = this._mapper.Map<MedicamentoProveedor>(medicamentoProveedorDto);
        this._unitOfWork.MedicamentoProveedores.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        medicamentoProveedorDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = medicamentoProveedorDto.Id}, medicamentoProveedorDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MedicamentoProveedorDto>> Put(int id, [FromBody]MedicamentoProveedorDto medicamentoProveedorDto){
        if(medicamentoProveedorDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<MedicamentoProveedor>(medicamentoProveedorDto);
        _unitOfWork.MedicamentoProveedores.Update(entidad);
        await _unitOfWork.SaveAsync();
        return medicamentoProveedorDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.MedicamentoProveedores.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.MedicamentoProveedores.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}