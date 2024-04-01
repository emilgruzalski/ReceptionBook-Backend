using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserForUpdateDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }
        public string Email { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public ICollection<string>? Roles { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
