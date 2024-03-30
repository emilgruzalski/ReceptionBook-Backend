namespace Shared.RequestFeatures;

public class RoomParameters : RequestParameters
{
    public RoomParameters() => OrderBy = "number";

    public string? Type { get; set; }

    public string? SearchTerm { get; set; }
}