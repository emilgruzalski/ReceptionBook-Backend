using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Reservation
    {
        [Column("ReservationId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Reservation start date is a required field.")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "Reservation end date is a required field.")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "Reservation total price is a required field.")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Reservation status is a required field.")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Reservation customer id is a required field.")]
        [ForeignKey(nameof(Customer))]
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required(ErrorMessage = "Reservation room id is a required field.")]
        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
