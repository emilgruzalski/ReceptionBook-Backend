using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Room
    {
        [Column("RoomId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Room number is a required field.")]
        [MaxLength(5, ErrorMessage = "Maximum length for the Number is 5 characters.")]
        public string? Number { get; set; }

        [Required(ErrorMessage = "Room type is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Type is 20 characters.")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Room price is a required field.")]
        public decimal Price { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }

        public ICollection<Maintenance>? Maintenances { get; set; }
    }
}
