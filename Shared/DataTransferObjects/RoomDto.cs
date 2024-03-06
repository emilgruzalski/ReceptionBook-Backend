namespace Shared.DataTransferObjects;

[Serializable]
public record RoomDto(Guid Id, string Number, string Type, decimal Price);

