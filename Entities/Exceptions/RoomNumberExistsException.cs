using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class RoomNumberExistsException : BadRequestException
    {
        public RoomNumberExistsException(string roomNumber)
        : base($"The room with number {roomNumber} already exists in the database.")
        {
        }
    }
}
