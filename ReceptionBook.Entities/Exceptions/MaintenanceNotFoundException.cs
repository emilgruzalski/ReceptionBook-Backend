namespace ReceptionBook.Entities.Exceptions;

public class MaintenanceNotFoundException : NotFoundException
{
    public MaintenanceNotFoundException(Guid maintenanceId)
        : base($"Maintenance with id {maintenanceId} doesn't exist in the database.")
    {
    }
}