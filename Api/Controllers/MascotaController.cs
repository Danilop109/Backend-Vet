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
    public class MascotaController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MascotaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MascotaDto>>> Get()
    {
        var entidad = await _unitOfWork.Mascotas.GetAllAsync();
        return _mapper.Map<List<MascotaDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MascotaDto>> Get(int id)
    {
        var entidad = await _unitOfWork.Mascotas.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<MascotaDto>(entidad);
    }

    [HttpGet("GetPetEspecie")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Mascota>> GetPetEspecieConsulta()
    {
        var entidad = await _unitOfWork.Mascotas.GetPetEspecie();
        var dto = _mapper.Map<IEnumerable<Mascota>>(entidad);
        return Ok(dto);
    }

    [HttpGet("GetPetEspecie2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Mascota>> GetPetEspecieConsulta2()
    {
        var entidad = await _unitOfWork.Mascotas.GetPetEspecie2();
        var dto = _mapper.Map<IEnumerable<Mascota>>(entidad);
        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Mascota>> Post(MascotaDto mascotaDto)
    {
        var entidad = this._mapper.Map<Mascota>(mascotaDto);
        this._unitOfWork.Mascotas.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        mascotaDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = mascotaDto.Id}, mascotaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MascotaDto>> Put(int id, [FromBody]MascotaDto mascotaDto){
        if(mascotaDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<Mascota>(mascotaDto);
        _unitOfWork.Mascotas.Update(entidad);
        await _unitOfWork.SaveAsync();
        return mascotaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.Mascotas.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.Mascotas.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}