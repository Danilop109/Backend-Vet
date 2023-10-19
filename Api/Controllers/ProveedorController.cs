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
    public class ProveedorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get()
    {
        var entidad = await _unitOfWork.Proveedores.GetAllAsync();
        return _mapper.Map<List<ProveedorDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProveedorDto>> Get(int id)
    {
        var entidad = await _unitOfWork.Proveedores.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return _mapper.Map<ProveedorDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProveedorDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await _unitOfWork.Proveedores.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = _mapper.Map<List<ProveedorDto>>(entidad.registros);
        return new Pager<ProveedorDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Proveedor>> Post(ProveedorDto proveedorDto)
    {
        var entidad = this._mapper.Map<Proveedor>(proveedorDto);
        this._unitOfWork.Proveedores.Add(entidad);
        await _unitOfWork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        proveedorDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = proveedorDto.Id}, proveedorDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProveedorDto>> Put(int id, [FromBody]ProveedorDto proveedorDto){
        if(proveedorDto == null)
        {
            return NotFound();
        }
        var entidad = this._mapper.Map<Proveedor>(proveedorDto);
        _unitOfWork.Proveedores.Update(entidad);
        await _unitOfWork.SaveAsync();
        return proveedorDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var entidad = await _unitOfWork.Proveedores.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        _unitOfWork.Proveedores.Remove(entidad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
}