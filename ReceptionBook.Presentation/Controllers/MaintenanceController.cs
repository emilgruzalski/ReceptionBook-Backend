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
    public IActionResult GetMaintenancesForRoom(Guid roomId)
    {
        var maintenance = _service.MaintenanceService.GetMaintenances(roomId, trackChanges: false);

        return Ok(maintenance);
    }

    [HttpGet("{id:guid}", Name = "GetMaintenanceForRoom")]
    public IActionResult GetMaintenanceForRoom(Guid roomId, Guid id)
    {
        var maintenance = _service.MaintenanceService.GetMaintenance(roomId, id, trackChanges: false);

        return Ok(maintenance);
    }
    
    [HttpPost]
    public IActionResult CreateMaintenanceForRoom(Guid roomId, [FromBody] MaintenanceForCreationDto maintenance)
    {
        if (maintenance is null)
            return BadRequest("MaintenanceForCreationDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        var maintenanceToReturn = _service.MaintenanceService.CreateMaintenanceForRoom(roomId, maintenance, trackChanges: false);
        
        return CreatedAtRoute("GetMaintenanceForRoom", new { roomId, id = maintenanceToReturn.Id },
            maintenanceToReturn);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteMaintenanceForRoom(Guid roomId, Guid id)
    {
        _service.MaintenanceService.DeleteMaintenanceForRoom(roomId, id, trackChanges: false);
        
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult UpdateMaintenanceForRoom(Guid roomId, Guid id, [FromBody] MaintenanceForUpdateDto maintenance)
    {
        if (maintenance is null)
            return BadRequest("MaintenanceForUpdateDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        _service.MaintenanceService.UpdateMaintenanceForRoom(roomId, id, maintenance, roomTrackChanges: false, mainTrackChanges: true);
        
        return NoContent();
    }
}