using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ReceptionBook.Presentation.Controllers;

[Route("api/reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ReservationsController(IServiceManager service) => _service = service;
    
    [HttpGet]
    public IActionResult GetReservations()
    {
        var reservations = _service.ReservationService.GetAllReservations(trackChanges: false);
        
        return Ok(reservations);
    }
    
    [HttpGet("{id:guid}", Name = "ReservationById")]
    public IActionResult GetReservation(Guid id)
    {
        var reservation = _service.ReservationService.GetReservation(id, trackChanges: false);
        
        return Ok(reservation);
    }
    
    [HttpPost]
    public IActionResult CreateReservation([FromBody] ReservationForCreationDto reservation)
    {
        if (reservation is null)
            return BadRequest("ReservationForCreationDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        var createdReservation = _service.ReservationService.CreateReservation(reservation);
        
        return CreatedAtRoute("ReservationById", new { id = createdReservation.Id }, createdReservation);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteReservation(Guid id)
    {
        _service.ReservationService.DeleteReservation(id, trackChanges: false);
        
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult UpdateReservation(Guid id, [FromBody] ReservationForUpdateDto reservation)
    {
        if (reservation is null)
            return BadRequest("ReservationForUpdateDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        _service.ReservationService.UpdateReservation(id, reservation, trackChanges: true);
        
        return NoContent();
    }
}