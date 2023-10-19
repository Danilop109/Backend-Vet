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
    public class TratamientoMedicoController : BaseApiController
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TratamientoMedicoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TratamientoMedicoDto>>> Get()
    {
        var entidad = await _unitOfWork.TratamientoMedicos.GetAllAsync();
        return _mapper.Map<List<TratamientoMedicoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TratamientoMedicoDto>> Get(int id)
    {
        var entidad = await _unitOfWork.TratamientoMedicos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<TratamientoMedicoDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TratamientoMedico>> Post(TratamientoMedicoDto tratamientoMedicoDto)
    {
        var entidad = this._mapper.Map<TratamientoMedico>(tratamientoMedicoDto);
        this._unitOfWork.TratamientoMedicos.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        tratamientoMedicoDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = tratamientoMedicoDto.Id}, tratamientoMedicoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TratamientoMedicoDto>> Put(int id, [FromBody]TratamientoMedicoDto tratamientoMedicoDto){
        if(tratamientoMedicoDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<TratamientoMedico>(tratamientoMedicoDto);
        _unitOfWork.TratamientoMedicos.Update(entidad);
        await _unitOfWork.SaveAsync();
        return tratamientoMedicoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.TratamientoMedicos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.TratamientoMedicos.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}