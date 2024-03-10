using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ReceptionBook.Presentation.Controllers;

[Route("api/rooms/{roomId}/maintenance")]
[ApiController]
public class MaintenanceController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public MaintenanceController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetMaintenancesForRoom(Guid roomId)
    {
        var maintenance = await _service.MaintenanceService.GetMaintenancesAsync(roomId, trackChanges: false);

        return Ok(maintenance);
    }

    [HttpGet("{id:guid}", Name = "GetMaintenanceForRoom")]
    public async Task<IActionResult> GetMaintenanceForRoom(Guid roomId, Guid id)
    {
        var maintenance = await _service.MaintenanceService.GetMaintenanceAsync(roomId, id, trackChanges: false);

        return Ok(maintenance);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMaintenanceForRoom(Guid roomId, [FromBody] MaintenanceForCreationDto maintenance)
    {
        if (maintenance is null)
            return BadRequest("MaintenanceForCreationDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        var maintenanceToReturn = await _service.MaintenanceService.CreateMaintenanceForRoomAsync(roomId, maintenance, trackChanges: false);
        
        return CreatedAtRoute("GetMaintenanceForRoom", new { roomId, id = maintenanceToReturn.Id },
            maintenanceToReturn);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMaintenanceForRoom(Guid roomId, Guid id)
    {
        await _service.MaintenanceService.DeleteMaintenanceForRoomAsync(roomId, id, trackChanges: false);
        
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMaintenanceForRoom(Guid roomId, Guid id, [FromBody] MaintenanceForUpdateDto maintenance)
    {
        if (maintenance is null)
            return BadRequest("MaintenanceForUpdateDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        await _service.MaintenanceService.UpdateMaintenanceForRoomAsync(roomId, id, maintenance, roomTrackChanges: false, mainTrackChanges: true);
        
        return NoContent();
    }
}