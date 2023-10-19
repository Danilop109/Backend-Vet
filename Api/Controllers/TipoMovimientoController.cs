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
    public class TipoMovimientoController : BaseApiController
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoMovimientoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoMovimientoDto>>> Get()
    {
        var entidad = await _unitOfWork.TipoMovimientos.GetAllAsync();
        return _mapper.Map<List<TipoMovimientoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoMovimientoDto>> Get(int id)
    {
        var entidad = await _unitOfWork.TipoMovimientos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<TipoMovimientoDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoMovimientoDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await _unitOfWork.TipoMovimientos.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = _mapper.Map<List<TipoMovimientoDto>>(entidad.registros);
        return new Pager<TipoMovimientoDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoMovimiento>> Post(TipoMovimientoDto tipoMovimientoDto)
    {
        var entidad = this._mapper.Map<TipoMovimiento>(tipoMovimientoDto);
        this._unitOfWork.TipoMovimientos.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        tipoMovimientoDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = tipoMovimientoDto.Id}, tipoMovimientoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoMovimientoDto>> Put(int id, [FromBody]TipoMovimientoDto tipoMovimientoDto){
        if(tipoMovimientoDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<TipoMovimiento>(tipoMovimientoDto);
        _unitOfWork.TipoMovimientos.Update(entidad);
        await _unitOfWork.SaveAsync();
        return tipoMovimientoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.TipoMovimientos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoMovimientos.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}