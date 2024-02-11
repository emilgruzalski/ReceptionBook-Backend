using Microsoft.AspNetCore.Mvc;
using ReceptionBook.Service.Contracts;

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
            try
            {
                var rooms = _service.RoomService.GetAllRooms(trackChanges: false);

                return Ok(rooms);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
