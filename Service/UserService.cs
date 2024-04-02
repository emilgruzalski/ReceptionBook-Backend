using AutoMapper;
using Contracts;
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
        private readonly IConfiguration _configuration;

        private User? _user;

        public UserService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }
        public Task DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserWithDetailsAsync(Guid roomId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetUsersAsync(bool trackChanges)
        {
            var users = _userManager.Users;

            return Task.FromResult(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        public Task UpdateUserAsync(Guid userId, UserForUpdateDto user, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
