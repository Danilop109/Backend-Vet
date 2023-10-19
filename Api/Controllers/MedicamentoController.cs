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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{   
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    // [Authorize]
    public class MedicamentoController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicamentoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get()
    {
        var entidad = await _unitOfWork.Medicamentos.GetAllAsync();
        return _mapper.Map<List<MedicamentoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MedicamentoDto>> Get(int id)
    {
        var entidad = await _unitOfWork.Medicamentos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<MedicamentoDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicamentoDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await _unitOfWork.Medicamentos.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = _mapper.Map<List<MedicamentoDto>>(entidad.registros);
        return new Pager<MedicamentoDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicamento>> Post(MedicamentoDto medicamentoDto)
    {
        var entidad = this._mapper.Map<Medicamento>(medicamentoDto);
        this._unitOfWork.Medicamentos.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        medicamentoDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = medicamentoDto.Id}, medicamentoDto);
    }


    //CONSULTA 2
    [HttpGet("GetMediFromLab")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetMediFromLabConsulta2()
    {
        var entidad = await _unitOfWork.Medicamentos.GetMediFromLab();
        var dto = _mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("GetMediFromLab")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> GetMediFromLabConsulta2Pag([FromQuery] Params Parameters)
    {
        var entidad = await _unitOfWork.Medicamentos.GetMediFromLab(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = _mapper.Map<List<object>>(entidad.registros);
        return Ok(new Pager<object>(listEntidad, entidad.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));
    }

    //CONSULTA 5
    [HttpGet("GetMedi50000")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicamento>> GetMedi50000Consulta5()
    {
        var entidad = await _unitOfWork.Medicamentos.GetMedi50000();
        var dto = _mapper.Map<IEnumerable<Medicamento>>(entidad);
        return Ok(dto);
    }

    [HttpGet("GetMedi50000")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<Medicamento>>> GetMedi50000Consulta5Pag([FromQuery] Params Parameters)
    {
        var entidad = await _unitOfWork.Medicamentos.GetMedi50000(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = _mapper.Map<List<Medicamento>>(entidad.registros);
        return Ok(new Pager<Medicamento>(listEntidad, entidad.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MedicamentoDto>> Put(int id, [FromBody]MedicamentoDto medicamentoDto){
        if(medicamentoDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<Medicamento>(medicamentoDto);
        _unitOfWork.Medicamentos.Update(entidad);
        await _unitOfWork.SaveAsync();
        return medicamentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.Medicamentos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.Medicamentos.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}