using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Maintenance
    {
        [Column("MaintenanceId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Maintenance start date is a required field.")]
        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [Required(ErrorMessage = "Maintenance description is a required field.")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Maintenance cost is a required field.")]
        public decimal? Cost { get; set; }

        [Required(ErrorMessage = "Maintenance room id is a required field.")]
        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
