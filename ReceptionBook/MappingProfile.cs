using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace ReceptionBook
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDto>();
            
            CreateMap<RoomForCreationDto, Room>();
            
            CreateMap<RoomForUpdateDto, Room>();
            
            CreateMap<Maintenance, MaintenanceDto>();
            
            CreateMap<MaintenanceForCreationDto, Maintenance>();

            CreateMap<MaintenanceForUpdateDto, Maintenance>();

            CreateMap<Reservation, ReservationDto>()
                .ConstructUsing(r => new ReservationDto(
                    r.Id,
                    r.StartDate,
                    r.EndDate,
                    r.Status,
                    r.TotalPrice,
                    $"{r.Customer.FirstName} {r.Customer.LastName}",
                    r.Room.Number));
            
            CreateMap<ReservationForCreationDto, Reservation>();
            
            CreateMap<ReservationForUpdateDto, Reservation>();
            
            CreateMap<Customer, CustomerDto>();
            
            CreateMap<CustomerForCreationDto, Customer>();
            
            CreateMap<CustomerForUpdateDto, Customer>();
        }
    }
}
