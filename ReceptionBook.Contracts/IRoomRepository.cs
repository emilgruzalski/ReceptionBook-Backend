using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceptionBook.Entities.Models;

namespace ReceptionBook.Contracts
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAllRooms(bool trackChanges);
    }
}
