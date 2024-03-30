namespace Shared.RequestFeatures;

public class CustomerParameters : RequestParameters
{
    public CustomerParameters() => OrderBy = "lastName";

    public string? SearchTerm { get; set; }
}