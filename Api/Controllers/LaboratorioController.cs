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
    public class LaboratorioController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LaboratorioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<LaboratorioDto>>> Get()
    {
        var entidad = await _unitOfWork.Laboratorios.GetAllAsync();
        return _mapper.Map<List<LaboratorioDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<LaboratorioDto>> Get(int id)
    {
        var entidad = await _unitOfWork.Laboratorios.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<LaboratorioDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<LaboratorioDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await _unitOfWork.Laboratorios.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = _mapper.Map<List<LaboratorioDto>>(entidad.registros);
        return new Pager<LaboratorioDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Laboratorio>> Post(LaboratorioDto laboratorioDto)
    {
        var entidad = this._mapper.Map<Laboratorio>(laboratorioDto);
        this._unitOfWork.Laboratorios.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        laboratorioDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = laboratorioDto.Id}, laboratorioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<LaboratorioDto>> Put(int id, [FromBody]LaboratorioDto laboratorioDto){
        if(laboratorioDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<Laboratorio>(laboratorioDto);
        _unitOfWork.Laboratorios.Update(entidad);
        await _unitOfWork.SaveAsync();
        return laboratorioDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.Laboratorios.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.Laboratorios.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}