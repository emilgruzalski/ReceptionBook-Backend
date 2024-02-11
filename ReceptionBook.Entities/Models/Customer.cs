﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReceptionBook.Entities.Models
{
    public class Customer
    {
        [Column("CustomerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer first name is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the FirstName is 20 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Customer last name is a required field.")]
        [MaxLength(40, ErrorMessage = "Maximum length for the LastName is 40 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Customer email is a required field.")]
        [MaxLength(40, ErrorMessage = "Maximum length for the Email is 20 characters.")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        public string? Email { get; set; }

        [MaxLength(20, ErrorMessage = "Maximum length for the PhoneNumber is 20 characters.")]
        [Phone(ErrorMessage = "Phone number is not a valid phone number.")]
        public string? PhoneNumber { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
