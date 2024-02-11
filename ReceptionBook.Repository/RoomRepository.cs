using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceptionBook.Contracts;
using ReceptionBook.Entities.Models;

namespace ReceptionBook.Repository
{
    internal sealed class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Room> GetAllRooms(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Number)
                .ToList();
    }
}
