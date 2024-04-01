using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersRoomsAsync(bool trackChanges);
        Task<UserDto> GetRoomWithDetailsAsync(Guid roomId, bool trackChanges);
        Task UpdateUserAsync(Guid userId, UserForUpdateDto user, bool trackChanges);
        Task DeleteUserAsync(Guid userId);
    }
}
