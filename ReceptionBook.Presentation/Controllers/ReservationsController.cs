using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ReceptionBook.Presentation.Controllers;

[Route("api")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ReservationsController(IServiceManager service) => _service = service;
    
    [Route("reservations")]
    [HttpGet]
    public IActionResult GetReservations()
    {
        var reservations = _service.ReservationService.GetAllReservations(trackChanges: false);
        
        return Ok(reservations);
    }
    
    [HttpGet("reservations/{id:guid}", Name = "ReservationById")]
    public IActionResult GetReservation(Guid id)
    {
        var reservation = _service.ReservationService.GetReservation(id, trackChanges: false);
        
        return Ok(reservation);
    }
    
    [HttpGet("rooms/{roomId}/reservations")]
    public IActionResult GetReservationsForRoom(Guid roomId)
    {
        var reservations = _service.ReservationService.GetReservationsForRoom(roomId, trackChanges: false);
        
        return Ok(reservations);
    }
    
    [HttpGet("rooms/{roomId}/reservations/{id:guid}")]
    public IActionResult GetReservationForRoom(Guid roomId, Guid id)
    {
        var reservation = _service.ReservationService.GetReservationForRoom(roomId, id, trackChanges: false);
        
        return Ok(reservation);
    }
    
    [HttpGet("customers/{customerId}/reservations")]
    public IActionResult GetReservationsForCustomer(Guid customerId)
    {
        var reservations = _service.ReservationService.GetReservationsForCustomer(customerId, trackChanges: false);
        
        return Ok(reservations);
    }
    
    [HttpGet("customers/{customerId}/reservations/{id:guid}")]
    public IActionResult GetReservationForCustomer(Guid customerId, Guid id)
    {
        var reservation = _service.ReservationService.GetReservationForCustomer(customerId, id, trackChanges: false);
        
        return Ok(reservation);
    }
    
    [HttpPost("reservations")]
    public IActionResult CreateReservation([FromBody] ReservationForCreationDto reservation)
    {
        if (reservation is null)
            return BadRequest("ReservationForCreationDto object is null");
        
        var createdReservation = _service.ReservationService.CreateReservation(reservation);
        
        return CreatedAtRoute("ReservationById", new { id = createdReservation.Id }, createdReservation);
    }
}