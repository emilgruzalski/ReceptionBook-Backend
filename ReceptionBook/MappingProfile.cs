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
            
            CreateMap<Maintenance, MaintenanceDto>();
            
            CreateMap<MaintenanceForCreationDto, Maintenance>();

            CreateMap<Reservation, ReservationDto>()
                .ConstructUsing(r => new ReservationDto(
                    r.Id,
                    r.StartDate,
                    r.EndDate,
                    r.Status,
                    r.TotalPrice,
                    $"{r.Customer.FirstName} {r.Customer.LastName}",
                    r.Room.Number));
            
            CreateMap<Reservation, ReservationForRoomDto>()
                .ConstructUsing(r => new ReservationForRoomDto(
                    r.Id,
                    r.StartDate,
                    r.EndDate,
                    $"{r.Customer.FirstName} {r.Customer.LastName}",
                    r.Status,
                    r.TotalPrice));
            
            CreateMap<Reservation, ReservationForCustomerDto>()
                .ForMember(r => r.RoomNumber, opt => opt.MapFrom(x => x.Room.Number));
            
            CreateMap<ReservationForCreationDto, Reservation>();
            
            CreateMap<Customer, CustomerDto>();
            
            CreateMap<CustomerForCreationDto, Customer>();
        }
    }
}
