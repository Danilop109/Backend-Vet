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
    public class MovimientoMedicamentoController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovimientoMedicamentoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MovimientoMedicamentoDto>>> Get()
    {
        var entidad = await _unitOfWork.MovimientoMedicamentos.GetAllAsync();
        return _mapper.Map<List<MovimientoMedicamentoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MovimientoMedicamentoDto>> Get(int id)
    {
        var entidad = await _unitOfWork.MovimientoMedicamentos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<MovimientoMedicamentoDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MovimientoMedicamento>> Post(MovimientoMedicamentoDto movimientoMedicamentoDto)
    {
        var entidad = this._mapper.Map<MovimientoMedicamento>(movimientoMedicamentoDto);
        this._unitOfWork.MovimientoMedicamentos.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        movimientoMedicamentoDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = movimientoMedicamentoDto.Id}, movimientoMedicamentoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MovimientoMedicamentoDto>> Put(int id, [FromBody]MovimientoMedicamentoDto movimientoMedicamentoDto){
        if(movimientoMedicamentoDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<MovimientoMedicamento>(movimientoMedicamentoDto);
        _unitOfWork.MovimientoMedicamentos.Update(entidad);
        await _unitOfWork.SaveAsync();
        return movimientoMedicamentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.MovimientoMedicamentos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.MovimientoMedicamentos.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}