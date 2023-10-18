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
    public class VeterinarioController : Controller
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VeterinarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
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