namespace Shared.DataTransferObjects;

[Serializable]
public record CustomerDto(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber);