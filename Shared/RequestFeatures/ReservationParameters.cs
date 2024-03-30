namespace Shared.RequestFeatures;

public class ReservationParameters : RequestParameters
{
    public ReservationParameters() => OrderBy = "startDate";

    public string? Status { get; set; }

    public string? SearchTerm { get; set; }
}