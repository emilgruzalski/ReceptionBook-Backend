using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record ReservationForManipulationDto : IValidatableObject
{
    [Required(ErrorMessage = "Reservation start date is a required field")]
    public DateOnly StartDate { get; init; }
    
    [Required(ErrorMessage = "Reservation end date is a required field")]
    public DateOnly EndDate { get; init; }
    
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Total price is required and it can't be lower than 0")]
    public Decimal TotalPrice { get; init; }

    [Required(ErrorMessage = "Reservation customer id is a required field")]
    public Guid CustomerId { get; init; }
    
    [Required(ErrorMessage = "Reservation room id is a required field")]
    public Guid RoomId { get; init; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate > EndDate)
        {
            yield return new ValidationResult("The end date must be greater than the start date.", new[] { "EndDate" });
        }
    }
}