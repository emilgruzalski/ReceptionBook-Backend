using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class UserService : IUserService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        //private readonly IConfiguration _configuration;

        private User? _user;

        public UserService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager)//, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            //_configuration = configuration;
        }
        
        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            await _userManager.DeleteAsync(user);
        }

        public Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = _userManager.Users;

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Task.FromResult(usersDto);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var userDto = _mapper.Map<UserDto>(user);

            var roles = await _userManager.GetRolesAsync(user);

            userDto.Roles = roles;

            return userDto;
        }

        public async Task UpdateUserAsync(Guid userId, UserForUpdateDto user)
        {
            var userEntity = await _userManager.FindByIdAsync(userId.ToString());

            if (userEntity is null)
                throw new RoomNotFoundException(userId);

            _mapper.Map(user, userEntity);

            var roles = await _userManager.GetRolesAsync(userEntity);

            await _userManager.RemoveFromRolesAsync(userEntity, roles);

            List<string> RolesToBeAssigned = user.Roles.ToList();

            await _userManager.AddToRolesAsync(userEntity, user.Roles);

            await _userManager.UpdateAsync(userEntity);
        }
    }
}
