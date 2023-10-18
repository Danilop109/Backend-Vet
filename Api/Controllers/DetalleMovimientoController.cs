using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    public class DetalleMovimientoController : BaseApiController
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DetalleMovimientoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DetalleMovimientoDto>>> Get()
    {
        var entidad = await _unitOfWork.DetalleMovimientos.GetAllAsync();
        return _mapper.Map<List<DetalleMovimientoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetalleMovimientoDto>> Get(int id)
    {
        var entidad = await _unitOfWork.DetalleMovimientos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<DetalleMovimientoDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DetalleMovimiento>> Post(DetalleMovimientoDto detalleMovimientoDto)
    {
        var entidad = this._mapper.Map<DetalleMovimiento>(detalleMovimientoDto);
        this._unitOfWork.DetalleMovimientos.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        detalleMovimientoDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = detalleMovimientoDto.Id}, detalleMovimientoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetalleMovimientoDto>> Put(int id, [FromBody]DetalleMovimientoDto detalleMovimientoDto){
        if(detalleMovimientoDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<DetalleMovimiento>(detalleMovimientoDto);
        _unitOfWork.DetalleMovimientos.Update(entidad);
        await _unitOfWork.SaveAsync();
        return detalleMovimientoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.DetalleMovimientos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.DetalleMovimientos.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}