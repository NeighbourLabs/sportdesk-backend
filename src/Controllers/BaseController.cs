using Microsoft.AspNetCore.Mvc;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;
using sportdesk_backend.Mappers;
using sportdesk_backend.Services;

namespace sportdesk_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<E, D, S>(S service, IMapperBase<E, D> mapper) : ControllerBase
    where E : EntityBase
    where D : DtoBase
    where S : IServiceBase<E>
{
    protected Guid TenantId
    {
        get
        {
            var claim = User.FindFirst("tenantId")?.Value;
            return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
        }
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<D>>> GetAll()
    {
        var entities = await service.GetAllAsync(TenantId);
        
        return Ok(entities.Select(e => mapper.MapToDto(e, TenantId)));
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<ActionResult<D>> GetById(Guid id)
    {
        var entity = await service.GetByIdAsync(id, TenantId);
        
        if (entity == null) return NotFound();
        
        return Ok(mapper.MapToDto(entity, TenantId));
    }

    [HttpPost]
    public virtual async Task<ActionResult<D>> Create(D dto)
    {
        var entity = mapper.MapToEntity(dto, TenantId);
        
        var createdEntity = await service.CreateAsync(entity);
        
        var resultDto = mapper.MapToDto(createdEntity, TenantId);
        
        return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id:guid}")]
    public virtual async Task<ActionResult<D>> Update(Guid id, D dto)
    {
        if (id != dto.Id) return BadRequest();
        
        var entity = mapper.MapToEntity(dto, TenantId);
        
        var updatedEntity = await service.UpdateAsync(entity, TenantId);
        
        return Ok(mapper.MapToDto(updatedEntity, TenantId));
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        await service.DeleteAsync(id, TenantId);
        
        return NoContent();
    }
}