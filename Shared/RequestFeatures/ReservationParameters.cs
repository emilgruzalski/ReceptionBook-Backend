namespace Shared.RequestFeatures;

public class ReservationParameters : RequestParameters
{
    public string? Status { get; set; }

    public string? SearchTerm { get; set; }
}