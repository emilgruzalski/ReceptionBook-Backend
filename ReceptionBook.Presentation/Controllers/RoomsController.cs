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
        public IActionResult GetRooms()
        {
            var rooms = _service.RoomService.GetAllRooms(trackChanges: false);

            return Ok(rooms);
        }
        
        [HttpGet("{id:guid}", Name = "RoomById")]
        public IActionResult GetRoom(Guid id)
        {
            var room = _service.RoomService.GetRoom(id, trackChanges: false);
            
            return Ok(room);
        }
        
        [HttpPost]
        public IActionResult CreateRoom([FromBody] RoomForCreationDto room)
        {
            if (room is null)
                return BadRequest("RoomForCreationDto object is null");
            
            var createdRoom = _service.RoomService.CreateRoom(room);
            
            return CreatedAtRoute("RoomById", new { id = createdRoom.Id }, createdRoom);
        }
        
        [HttpGet("collection/({ids})", Name = "RoomsCollection")]
        public IActionResult GetRoomsCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var rooms = _service.RoomService.GetByIds(ids, trackChanges: false);
            
            return Ok(rooms);
        }
        
        [HttpGet("free")]
        public IActionResult GetFreeRooms([FromBody] AvailableRoomsDto room)
        {
            var freeRooms = _service.RoomService.GetAvailableRooms(room, trackChanges: false);
            
            return Ok(freeRooms);
        }
        
        [HttpPost("collection")]
        public IActionResult CreateRoomCollection([FromBody] IEnumerable<RoomForCreationDto> roomCollection)
        {
            var result = _service.RoomService.CreateRoomCollection(roomCollection);
            
            return CreatedAtRoute("RoomCollection", new { result.ids }, result.rooms);
        }
        
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteRoom(Guid id)
        {
            _service.RoomService.DeleteRoom(id, trackChanges: false);
            
            return NoContent();
        }
        
        [HttpPut("{id:guid}")]
        public IActionResult UpdateRoom(Guid id, [FromBody] RoomForUpdateDto room)
        {
            if (room is null)
                return BadRequest("RoomForUpdateDto object is null");
            
            _service.RoomService.UpdateRoom(id, room, trackChanges: true);
            
            return NoContent();
        }
        
        [HttpGet("{roomId}/reservations")]
        public IActionResult GetReservationsForRoom(Guid roomId)
        {
            var reservations = _service.ReservationService.GetReservationsForRoom(roomId, trackChanges: false);
        
            return Ok(reservations);
        }
    }
}
