using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
        
        [HttpGet("{id:guid}")]
        public IActionResult GetRoom(Guid id)
        {
            var room = _service.RoomService.GetRoom(id, trackChanges: false);
            
            return Ok(room);
        }
    }
}
