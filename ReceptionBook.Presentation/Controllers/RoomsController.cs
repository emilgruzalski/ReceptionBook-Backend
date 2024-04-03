using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReceptionBook.Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace ReceptionBook.Presentation.Controllers
{
    [Route("api/rooms")]
    //[Authorize(Roles = "Manager")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RoomsController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] RoomParameters roomParameters)
        {
            var pagedResult = await _service.RoomService.GetAllRoomsAsync(trackChanges: false, roomParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            
            return Ok(pagedResult.rooms);
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

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdRoom = await _service.RoomService.CreateRoomAsync(room);
            
            return CreatedAtRoute("RoomById", new { id = createdRoom.Id }, createdRoom);
        }
        
        [HttpGet("collection/({ids})", Name = "RoomsCollection")]
        public async Task<IActionResult> GetRoomsCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var rooms = await _service.RoomService.GetByIdsAsync(ids, trackChanges: false);
            
            return Ok(rooms);
        }
        
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms([FromQuery] AvailableRoomParameters roomParameters)
        {   
            var pagedResult = await _service.RoomService.GetAvailableRoomsAsync(roomParameters, trackChanges: false);
            
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            
            return Ok(pagedResult.rooms);
        }

        [HttpGet("available/{id:guid}")]
        public async Task<IActionResult> GetAvailableRoomsUpdate(Guid id, [FromQuery] AvailableRoomParameters roomParameters)
        {
            var pagedResult = await _service.RoomService.GetAvailableRoomsAsync(id, roomParameters, trackChanges: false);
            
            return Ok(pagedResult.rooms);
        }
        
        [HttpPost("collection")]
        public async Task<IActionResult> CreateRoomCollection([FromBody] IEnumerable<RoomForCreationDto> roomCollection)
        {
            var result = await _service.RoomService.CreateRoomCollectionAsync(roomCollection);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

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

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.RoomService.UpdateRoomAsync(id, room, trackChanges: true);
            
            return NoContent();
        }
    }
}
