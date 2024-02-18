using Microsoft.AspNetCore.Mvc;
using ReceptionBook.Service.Contracts;

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

    [HttpGet("{id:guid}")]
    public IActionResult GetMaintenanceForRoom(Guid roomId, Guid id)
    {
        var maintenance = _service.MaintenanceService.GetMaintenance(roomId, id, trackChanges: false);

        return Ok(maintenance);
    }
}