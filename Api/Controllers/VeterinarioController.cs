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
    public class VeterinarioController : BaseApiController
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VeterinarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<VeterinarioDto>>> Get()
    {
        var entidad = await _unitOfWork.Veterinarios.GetAllAsync();
        return _mapper.Map<List<VeterinarioDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<VeterinarioDto>> Get(int id)
    {
        var entidad = await _unitOfWork.Veterinarios.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<VeterinarioDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<VeterinarioDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await _unitOfWork.Veterinarios.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = _mapper.Map<List<VeterinarioDto>>(entidad.registros);
        return new Pager<VeterinarioDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }
    //CONSULTA 1
    [HttpGet("GetCirujanoVascular")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Veterinario>> GetPetEspecieConsulta1()
    {
        var entidad = await _unitOfWork.Veterinarios.GetCirujanoVascular();
        var dto = _mapper.Map<IEnumerable<Veterinario>>(entidad);
        return Ok(dto);
    }

    [HttpGet("GetCirujanoVascular")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<Veterinario>>> GetPetEspecieConsulta1Pag([FromQuery] Params Parameters)
    {
        var entidad = await _unitOfWork.Veterinarios.GetCirujanoVascular(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = _mapper.Map<List<Veterinario>>(entidad.registros);
        return Ok(new Pager<Veterinario>(listEntidad, entidad.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Veterinario>> Post(VeterinarioDto veterinarioDto)
    {
        var entidad = this._mapper.Map<Veterinario>(veterinarioDto);
        this._unitOfWork.Veterinarios.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        veterinarioDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = veterinarioDto.Id}, veterinarioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<VeterinarioDto>> Put(int id, [FromBody]VeterinarioDto veterinarioDto){
        if(veterinarioDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<Veterinario>(veterinarioDto);
        _unitOfWork.Veterinarios.Update(entidad);
        await _unitOfWork.SaveAsync();
        return veterinarioDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.Veterinarios.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.Veterinarios.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}