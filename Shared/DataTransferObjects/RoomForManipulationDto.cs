using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record RoomForManipulationDto
{
    [Required(ErrorMessage = "Room number is a required field")]
    [MaxLength(5, ErrorMessage = "Maximum length for the Number is 5 characters.")]
    public string Number { get; init; }
    
    [Required(ErrorMessage = "Room type is a required field")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Type is 20 characters.")]
    public string Type { get; init; }
    
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Price is required and it can't be lower than 0")]
    public decimal Price { get; init; }
    
    public IEnumerable<MaintenanceForCreationDto> Maintenances { get; init; }
}