using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Password is required")] 
        public string? Password { get; init; }
        [Required(ErrorMessage = "Email is required")] 
        public string? Email { get; init; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; init; }
    }
}
