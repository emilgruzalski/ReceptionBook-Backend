using Microsoft.AspNetCore.Mvc;
using ReceptionBook.Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ReceptionBook.Presentation.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RoomsController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _service.RoomService.GetAllRoomsAsync(trackChanges: false);

            return Ok(rooms);
        }
        
        [HttpGet("{id:guid}", Name = "RoomById")]
        public async Task<IActionResult> GetRoom(Guid id)
        {
            var room = await _service.RoomService.GetRoomAsync(id, trackChanges: false);
            
            return Ok(room);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomForCreationDto room)
        {
            if (room is null)
                return BadRequest("RoomForCreationDto object is null");
            
            var createdRoom = await _service.RoomService.CreateRoomAsync(room);
            
            return CreatedAtRoute("RoomById", new { id = createdRoom.Id }, createdRoom);
        }
        
        [HttpGet("collection/({ids})", Name = "RoomsCollection")]
        public async Task<IActionResult> GetRoomsCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var rooms = await _service.RoomService.GetByIdsAsync(ids, trackChanges: false);
            
            return Ok(rooms);
        }
        
        [HttpGet("free")]
        public async Task<IActionResult> GetFreeRooms(DateTime start, DateTime end, string type)
        {
            var room = new AvailableRoomsDto(type, start, end);
            
            var freeRooms = await _service.RoomService.GetAvailableRoomsAsync(room, trackChanges: false);
            
            return Ok(freeRooms);
        }
        
        [HttpPost("collection")]
        public async Task<IActionResult> CreateRoomCollection([FromBody] IEnumerable<RoomForCreationDto> roomCollection)
        {
            var result = await _service.RoomService.CreateRoomCollectionAsync(roomCollection);
            
            return CreatedAtRoute("RoomCollection", new { result.ids }, result.rooms);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            await _service.RoomService.DeleteRoomAsync(id, trackChanges: false);
            
            return NoContent();
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] RoomForUpdateDto room)
        {
            if (room is null)
                return BadRequest("RoomForUpdateDto object is null");
            
            await _service.RoomService.UpdateRoomAsync(id, room, trackChanges: true);
            
            return NoContent();
        }
        
        [HttpGet("{roomId}/reservations")]
        public async Task<IActionResult> GetReservationsForRoom(Guid roomId)
        {
            var reservations = await _service.ReservationService.GetReservationsForRoomAsync(roomId, trackChanges: false);
        
            return Ok(reservations);
        }
    }
}
