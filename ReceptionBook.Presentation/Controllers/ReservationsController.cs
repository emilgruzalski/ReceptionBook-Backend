using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace ReceptionBook.Presentation.Controllers;

[Route("api/reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ReservationsController(IServiceManager service) => _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetReservations([FromQuery] ReservationParameters reservationParameters)
    {
        var pagedResult = await _service.ReservationService.GetAllReservationsAsync(trackChanges: false, reservationParameters);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        
        return Ok(pagedResult.reservations);
    }
    
    [HttpGet("{id:guid}", Name = "ReservationById")]
    public async Task<IActionResult> GetReservation(Guid id)
    {
        var reservation = await _service.ReservationService.GetReservationAsync(id, trackChanges: false);
        
        return Ok(reservation);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationForCreationDto reservation)
    {
        if (reservation is null)
            return BadRequest("ReservationForCreationDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        var createdReservation = await _service.ReservationService.CreateReservationAsync(reservation);
        
        return CreatedAtRoute("ReservationById", new { id = createdReservation.Id }, createdReservation);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        await _service.ReservationService.DeleteReservationAsync(id, trackChanges: false);
        
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateReservation(Guid id, [FromBody] ReservationForUpdateDto reservation)
    {
        if (reservation is null)
            return BadRequest("ReservationForUpdateDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        await _service.ReservationService.UpdateReservationAsync(id, reservation, trackChanges: true);
        
        return NoContent();
    }
}