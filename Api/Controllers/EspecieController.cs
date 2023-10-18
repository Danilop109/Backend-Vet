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
    public class EspecieController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EspecieController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<EspecieDto>>> Get()
    {
        var entidad = await _unitOfWork.Especies.GetAllAsync();
        return _mapper.Map<List<EspecieDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EspecieDto>> Get(int id)
    {
        var entidad = await _unitOfWork.Especies.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<EspecieDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Especie>> Post(EspecieDto especieDto)
    {
        var entidad = this._mapper.Map<Especie>(especieDto);
        this._unitOfWork.Especies.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        especieDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = especieDto.Id}, especieDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EspecieDto>> Put(int id, [FromBody]EspecieDto especieDto){
        if(especieDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<Especie>(especieDto);
        _unitOfWork.Especies.Update(entidad);
        await _unitOfWork.SaveAsync();
        return especieDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.Especies.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.Especies.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}