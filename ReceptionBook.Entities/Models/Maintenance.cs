using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionBook.Entities.Models
{
    public class Maintenance
    {
        [Column("MaintenanceId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Maintenance start date is a required field.")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Maintenance description is a required field.")]
        public string? Description { get; set; }

        public decimal? Cost { get; set; }

        [Required(ErrorMessage = "Maintenance room id is a required field.")]
        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
