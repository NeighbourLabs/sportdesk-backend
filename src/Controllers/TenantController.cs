using Microsoft.AspNetCore.Mvc;
using sportdesk_backend.Dtos;
using sportdesk_backend.Mappers;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TenantController(ITenantService service, TenantMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TenantDto>>> GetAll()
    {
        var entities = await service.GetAllAsync();
        var dtos = entities.Select(mapper.MapToDto);
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TenantDto>> GetById(Guid id)
    {
        var entity = await service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        return Ok(mapper.MapToDto(entity));
    }

    [HttpPost]
    public async Task<ActionResult<TenantDto>> Create(TenantDto dto)
    {
        var entity = mapper.MapToEntity(dto);
        var createdEntity = await service.CreateAsync(entity);
        var resultDto = mapper.MapToDto(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TenantDto>> Update(Guid id, TenantDto dto)
    {
        if (id != dto.Id) return BadRequest("Invalid Id");

        var entity = mapper.MapToEntity(dto);
        var updatedEntity = await service.UpdateAsync(entity);
        
        return Ok(mapper.MapToDto(updatedEntity));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}