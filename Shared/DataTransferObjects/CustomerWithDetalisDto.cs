using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CustomerWithDetalisDto(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber, ICollection<ReservationShortDto> Reservations);
}
