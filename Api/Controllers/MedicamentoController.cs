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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicamento>> GetMediFromLabConsulta2()
    {
        var entidad = await _unitOfWork.Medicamentos.GetMediFromLab();
        var dto = _mapper.Map<IEnumerable<Medicamento>>(entidad);
        return Ok(dto);
    }

    //CONSULTA 5
    [HttpGet("GetMedi50000")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicamento>> GetMedi50000Consulta5()
    {
        var entidad = await _unitOfWork.Medicamentos.GetMedi50000();
        var dto = _mapper.Map<IEnumerable<Medicamento>>(entidad);
        return Ok(dto);
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