using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAllRooms(bool trackChanges);
        Room GetRoom(Guid roomId, bool trackChanges);
    }
}
