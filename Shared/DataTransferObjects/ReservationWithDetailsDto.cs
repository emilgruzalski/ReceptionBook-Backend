using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ReservationWithDetailsDto(Guid Id, DateOnly StartDate, DateOnly EndDate, Decimal TotalPrice, string Status, CustomerDto Customer, RoomDto Room);
}
