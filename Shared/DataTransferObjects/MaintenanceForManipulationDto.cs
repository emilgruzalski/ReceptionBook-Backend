using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record MaintenanceForManipulationDto : IValidatableObject
{
    [Required(ErrorMessage = "Maintenance description is a required field")]
    public string Description { get; init;}
    
    [Required(ErrorMessage = "Maintenance start date is a required field")]
    public DateOnly StartDate { get; init;}
    
    public DateOnly EndDate { get; init;}
    
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Cost is required and it can't be lower than 0")]
    public Decimal Cost { get; init;}
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate > EndDate)
        {
            yield return new ValidationResult("The end date must be greater than the start date.", new[] { "EndDate" });
        }
    }
}