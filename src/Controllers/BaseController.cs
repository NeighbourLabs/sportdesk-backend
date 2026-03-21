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
    // TODO: Extract TenantId from JWT bearer token once auth is implemented
    // protected Guid TenantId
    // {
    //     get
    //     {
    //         var claim = User.FindFirst("tenantId")?.Value;
    //         if (!Guid.TryParse(claim, out var id) || id == Guid.Empty)
    //             throw new UnauthorizedAccessException("Missing or invalid tenantId claim.");
    //         return id;
    //     }
    // }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<D>>> GetAll([FromQuery] Guid tenantId)
    {
        var entities = await service.GetAllAsync(tenantId);

        return Ok(entities.Select(e => mapper.MapToDto(e, tenantId)));
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<ActionResult<D>> GetById(Guid id, [FromQuery] Guid tenantId)
    {
        var entity = await service.GetByIdAsync(id, tenantId);

        if (entity == null) return NotFound();

        return Ok(mapper.MapToDto(entity, tenantId));
    }

    [HttpPost]
    public virtual async Task<ActionResult<D>> Create(D dto)
    {
        var entity = mapper.MapToEntity(dto, dto.TenantId);

        var createdEntity = await service.CreateAsync(entity);

        var resultDto = mapper.MapToDto(createdEntity, dto.TenantId);

        return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id:guid}")]
    public virtual async Task<ActionResult<D>> Update(Guid id, D dto)
    {
        if (id != dto.Id) return BadRequest();

        var entity = mapper.MapToEntity(dto, dto.TenantId);

        var updatedEntity = await service.UpdateAsync(entity, dto.TenantId);

        return Ok(mapper.MapToDto(updatedEntity, dto.TenantId));
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> Delete(Guid id, [FromQuery] Guid tenantId)
    {
        await service.DeleteAsync(id, tenantId);

        return NoContent();
    }
}