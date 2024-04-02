using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record RoomWithDetailsDto(Guid Id, string Number, string Type, decimal Price, ICollection<ReservationShortDto> Reservations);
}
