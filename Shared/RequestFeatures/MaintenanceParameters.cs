namespace Shared.RequestFeatures;

public class MaintenanceParameters : RequestParameters
{
    public MaintenanceParameters() => OrderBy = "startDate";

    public string? SearchTerm { get; set; }
}